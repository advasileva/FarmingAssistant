# -*- coding: utf-8 -*-
from weather import SimpleWeather


class WeatherForecaster:    # TODO

    @staticmethod
    def get_forecast(weather_list):

        for weather in weather_list:    # temp
            if weather is not None:
                return weather
        return SimpleWeather(25, 70, 1000)
