# -*- coding: utf-8 -*-
import requests
from os import environ
from dotenv import load_dotenv

load_dotenv()


class Location:
    def __init__(self, latitude: float, longitude: float):
        if latitude < -90 or 90 < latitude or longitude < -180 or 180 < longitude:
            raise ValueError
        self.latitude = round(latitude, 4)
        self.longitude = round(longitude, 4)


class SimpleWeather:

    def __init__(self, temperature: float, humidity: float, pressure: float):
        self.temperature = temperature
        self.humidity = humidity
        self.pressure = pressure


class DetailedWeather:

    def __init__(self, temperature, humidity, wind, pressure, cloudiness, description, forecasts=[]):
        self.temperature = temperature
        self.humidity = humidity
        self.wind = wind
        self.pressure = pressure
        self.cloudiness = cloudiness
        self.description = description
        self.forecasts = forecasts

    def get_object_dict(self):
        return self.__dict__


class WeatherAPI:

    @staticmethod
    def get_detailed(location: Location) -> DetailedWeather:
        try:
            key = environ['OPENWEATHERMAP_KEY']
            base_https_url = 'https://api.openweathermap.org/data/2.5/weather?'
            request_url = base_https_url + f'lat={location.latitude}&lon={location.longitude}&appid={key}&units=metric'
            response = requests.get(request_url).json()
            temperature = response['main']['temp']
            humidity = response['main']['humidity']
            wind = response['wind']['speed']
            pressure = response['main']['pressure']
            cloudiness = response['clouds']['all']
            description = response['weather'][0]['description']
            return DetailedWeather(temperature, humidity, wind, pressure, cloudiness, description)
        except:
            return None

    @staticmethod
    def get_all_simple_predictions(location: Location, allow_none: bool = False):
        get_weather_methods = [WeatherAPI.get_simple_prediction_openweathermap,
                               WeatherAPI.get_simple_prediction_weatherapi,
                               WeatherAPI.get_simple_prediction_weatherbit]
        result = []
        for get_weather in get_weather_methods:
            weather = get_weather(location)
            if weather is None and not allow_none:
                continue
            result.append(weather)
        return result

    # https://openweathermap.org/current
    @staticmethod
    def get_simple_prediction_openweathermap(location: Location) -> SimpleWeather:
        try:
            key = environ['OPENWEATHERMAP_KEY']
            base_https_url = 'https://api.openweathermap.org/data/2.5/onecall?'
            request_url = base_https_url + f'lat={location.latitude}&lon={location.longitude}&appid={key}' \
                f'&exclude=current,minutely,hourly,alerts&units=metric'
            response = requests.get(request_url).json()
            temperature = (response['daily'][1]['temp']['min'] + response['daily'][1]['temp']['max']) / 2
            humidity = response['daily'][1]['humidity']
            pressure = response['daily'][1]['pressure']
            return SimpleWeather(temperature, humidity, pressure)
        except:
            return None

    # https://www.weatherbit.io/api/weather-current
    @staticmethod
    def get_simple_prediction_weatherapi(location: Location) -> SimpleWeather:
        try:
            key = environ['WEATHERAPI_KEY']
            base_https_url = 'http://api.weatherapi.com/v1/forecast.json?'
            request_url = base_https_url + f'key={key}&q={location.latitude},{location.longitude}&days=2'
            response = requests.get(request_url).json()
            temperature = response['forecast']['forecastday'][1]['day']['avgtemp_c']
            humidity = response['forecast']['forecastday'][1]['day']['avghumidity']
            pressure = response['forecast']['forecastday'][1]['hour'][0]['pressure_mb']
            return SimpleWeather(temperature, humidity, pressure)
        except:
            return None

    # https://www.weatherbit.io/api/weather-current
    @staticmethod
    def get_simple_prediction_weatherbit(location: Location) -> SimpleWeather:
        try:
            key = environ['WEATHERBIT_KEY']
            base_https_url = 'https://api.weatherbit.io/v2.0/forecast/daily?'
            request_url = base_https_url + f'key={key}&lat={location.latitude}&lon={location.longitude}&days=2'
            response = requests.get(request_url).json()
            temperature = response['data'][1]['temp']
            humidity = response['data'][1]['rh']
            pressure = response['data'][1]['pres']
            return SimpleWeather(temperature, humidity, pressure)
        except:
            return None

    @staticmethod
    def get_simple_current_openweathermap(location: Location) -> SimpleWeather:
        try:
            key = environ['OPENWEATHERMAP_KEY']
            base_https_url = 'https://api.openweathermap.org/data/2.5/weather?'
            request_url = base_https_url + f'lat={location.latitude}&lon={location.longitude}&appid={key}&units=metric'
            response = requests.get(request_url).json()
            temperature = response['main']['temp']
            humidity = response['main']['humidity']
            pressure = response['main']['pressure']
            return SimpleWeather(temperature, humidity, pressure)
        except:
            return None

    @staticmethod
    def get_simple_current_weatherbit(location: Location) -> SimpleWeather:
        try:
            key = environ['WEATHERBIT_KEY']
            base_https_url = 'https://api.weatherbit.io/v2.0/current?'
            request_url = base_https_url + f'key={key}&lat={location.latitude}&lon={location.longitude}'
            response = requests.get(request_url).json()
            temperature = response['data'][0]['temp']
            humidity = response['data'][0]['rh']
            pressure = response['data'][0]['pres']
            return SimpleWeather(temperature, humidity, pressure)
        except:
            return None
