# -*- coding: utf-8 -*-
import re


class AccountUtils:    # Tested OK

    DEFAULT_CUSTOMER_INFO = '{}'

    @staticmethod
    def customer_info_is_correct(customer_info):
        customer_info_keys = ['Fields']
        if not AccountUtils.verify_json(customer_info, customer_info_keys):
            return False
        if type(customer_info['Fields']) is not list:
            return False
        for field in customer_info['Fields']:
            if not AccountUtils.field_is_correct(field):
                return False
        return True

    @staticmethod
    def field_is_correct(field, contains_all_keys=False):
        field_keys = ['Name', 'Latitude', 'Longitude', 'PlantName', 'PlantingDate']
        plant_names = ['None', 'Carrot', 'Corn', 'Potato', 'Tomato', 'Wheat']
        if not AccountUtils.verify_json(field, field_keys, contains_all_keys):
            return False
        if 'Name' in field.keys() and field['Name'] is not None:
            if not (type(field['Name']) is str and len(field['Name']) <= 50
                    and re.search(r'[\'\"\\/\f\n\r\t]', field['Name']) is None):
                return False
        if 'Latitude' in field.keys():
            if type(field['Latitude']) not in [int, float] or not (-90 <= field['Latitude'] <= 90):
                return False
        if 'Longitude' in field.keys():
            if type(field['Longitude']) not in [int, float] or not (-180 <= field['Longitude'] <= 180):
                return False
        if 'PlantName' in field.keys() and field['PlantName'] is not None:
            if type(field['PlantName']) is not str or field['PlantName'] not in plant_names:
                return False
        if 'PlantingDate' in field.keys():
            if type(field['PlantingDate']) is not int:
                return False
        return True

    @staticmethod
    def verify_json(data, allowed_keys, contains_all_allowed=False):
        if not type(data) is dict:
            return False
        for key in data.keys():
            if key not in allowed_keys:
                return False
        if contains_all_allowed:
            for key in allowed_keys:
                if key not in data.keys():
                    return False
        return True

    @staticmethod
    def username_format_is_correct(data):
        if type(data) is not str:
            return False
        return 6 <= len(data) <= 20 and re.search(r'[^a-z0-9]', data) is None

    @staticmethod
    def password_format_is_correct(data):
        if type(data) is not str:
            return False
        return 8 <= len(data) <= 40    # and re.search(r'[^A-Za-z0-9]', data) is None

    @staticmethod
    def token_format_is_correct(data):
        if type(data) is not str:
            return False
        return len(data) <= 64 and re.search(r'[^a-z0-9]', data) is None
