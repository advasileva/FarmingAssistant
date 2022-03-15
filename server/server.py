# -*- coding: utf-8 -*-
import socket
import json

from data_controller import DataController
from account_utils import AccountUtils
from crypto_utils import CryptoUtils
from recommendation import RecommendationMaker
from os import environ
from dotenv import load_dotenv

load_dotenv()


class Server:    # Tested OK

    HOST = environ['SERVER_HOST']
    PORT = int(environ['SERVER_PORT'])
    LISTENING_LIMIT = int(environ['LISTENING_LIMIT'])
    CLIENT_TIMEOUT = int(environ['CLIENT_TIMEOUT'])

    __aes_key = None
    __aes_iv = None

    @staticmethod
    def run():
        server = socket.socket(socket.AF_INET, socket.SOCK_STREAM, socket.IPPROTO_TCP)
        server.bind((Server.HOST, Server.PORT))
        server.listen(Server.LISTENING_LIMIT)
        while True:
            client, client_address = server.accept()
            client.settimeout(Server.CLIENT_TIMEOUT)
            Server.__handle_request(client)

    @staticmethod
    def __handle_request(client):
        try:
            token = Server.__decode(client.recv(64))
            request = json.loads(Server.__decode(client.recv(50000)))
            response = Server.__create_response(request, token)
            client.send(Server.__encode(response.to_json()))
        except:
            pass
        finally:
            client.close()

    @staticmethod
    def __create_response(request, token):
        try:
            if request['Type'] == 'SignUpRequest' or request['Type'] == 'SignInRequest':
                username = request['Username']
                password = request['Password']
                errors = []
                if not AccountUtils.username_format_is_correct(username):
                    errors.append('InvalidUsernameError')
                if not AccountUtils.password_format_is_correct(password):
                    errors.append('InvalidPasswordError')
                if len(errors):
                    return Response(errors=errors)

                pwd_hash = CryptoUtils.get_password_hash(password)

                if request['Type'] == 'SignUpRequest':
                    if DataController.user_exists(username):
                        return Response(errors='UserAlreadyExistsError')
                    DataController.add_user(username, AccountUtils.DEFAULT_CUSTOMER_INFO, pwd_hash)

                if request['Type'] == 'SignInRequest':
                    if not DataController.user_exists(username):
                        return Response(errors='UserNotFoundError')
                    if not DataController.verify_auth_by_pwd_hash(username, pwd_hash):
                        return Response(errors='PasswordAuthError')

                Server.__update_and_return_token(username)
                return Response(token=DataController.get_token(username))

            if not AccountUtils.token_format_is_correct(token):
                return Response(errors='TokenFormatError')
            username = DataController.get_username_by_token(token)
            new_token = Server.__update_and_return_token(username)
            response = Response(token=token if new_token is None else new_token)

            if request['Type'] == 'GetRecommendationsRequest':
                for field in request['TargetFields']:
                    if not AccountUtils.field_is_correct(field):
                        response.Errors = ['InvalidFieldError']
                        return response
                recommendations = RecommendationMaker.get_all_recommendations(request['TargetFields'])
                recommendations = [[r.get_object_dict() for r in arr] for arr in recommendations]
                response.Parameter = json.dumps(recommendations)

            else:
                if username is None:    # Token doesn't exist
                    return Response(errors='TokenAuthError')

                if request['Type'] == 'GetCustomerInfoRequest':
                    response.Parameter = json.dumps(DataController.get_customer_info(username))

                if request['Type'] == 'UpdateCustomerInfoRequest':
                    if not AccountUtils.customer_info_is_correct(request['CustomerInfo']):
                        response.Errors = ['InvalidCustomerInfoError']
                    else:
                        DataController.update_customer_info(username, request['CustomerInfo'])

            return response

        except:
            return Response(errors='ServerError')

    @staticmethod
    def __encode(data):
        return data.encode('utf-8')

    @staticmethod
    def __decode(data):
        return data.decode('utf-8')

    @staticmethod
    def __update_and_return_token(username):
        token = DataController.get_token(username)
        if token is not None and DataController.check_token_relevance(token):
            return None
        while True:
            new_token = CryptoUtils.generate_token()
            if not DataController.token_exists(new_token):
                DataController.add_token(username, new_token)
                return new_token


class Response:
    def __init__(self, errors=None, parameter: str = None, token: str = None):
        if type(errors) == list:
            self.Errors = errors
        elif type(errors) == str:
            self.Errors = [errors]
        else:
            self.Errors = []
        self.Parameter = parameter
        self.NewAuthToken = token

    def to_json(self):
        return json.dumps(self.__dict__)


if __name__ == "__main__":
    Server.run()
