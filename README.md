﻿# Содержание
0. [**Инструкция по использованию**](#bнструкция-по-использованию)  
 0.1. [*Инструкция по запуску приложения*](#инструкция-по-запуску-приложения)  
 0.2. [*Инструкция по запуску сервера*](#инструкция-по-запуску-сервера)  
1. [**Описание доменной области**](#описание-доменной-области)  
 1.1. [*Коротко о нашем проекте*](#коротко-о-нашем-проекте)  
 1.2. [*Введение*](#а-введение)  
 1.3. [*Клиенты и пользователи*](#б-клиенты-и-пользователи)  
 1.4. [*Окружение*](#в-окружение)  
 1.5. [*Как происходит прогнозирование погодных данных в настоящее время?*](#г-как-происходит-прогнозирование-погодных-данных-в-настоящее-время)  
 1.6. [*Конкурирующее программное обеспечение* ](#д-конкурирующее-программное-обеспечение )  
 1.7. [*Общие знания о сфере*](#е-общие-знания-о-сфере)  
2. [**Use Cases**](#use-cases)  
 2.1. [*Категории юзкейсов*](#категории-юзкейсов)  
 2.2. [*Запуск системы*](#1-запуск-системы)  
 2.3. [*Регистрация*](#2-регистрация)  
 2.4. [*Авторизация*](#3-авторизация)  
 2.5. [*Добавление поля*](#4-добавление-поля)  
 2.6. [*Получение рекомендаций по культурам для посадки*](#5-получение-рекомендаций-по-культурам-для-посадки)  
 2.7. [*Получение рекомендаций по срокам сбора урожая*](#6-получение-рекомендаций-по-срокам-сбора-урожая)  
3. [**Функциональные и нефункциональные требования**](#функциональные-и-нефункциональные-требования)  
 3.1. [*Функциональные*](#функциональные)  
 3.2. [*Нефункциональные*](#нефункциональные)  
5. [**Архитектура**](#архитектура)  
 4.1. [*Первоначальный вариант*](#первоначальный-вариант)  
 4.2. [*Компонентная модель*](#компонентная-модель)  
 4.3. [*Мобильное приложение*](#мобильное-приложение)   
 4.4. [*Сервер*](#сервер)   
 4.5. [*Модель машинного обучения*](#модель-машинного-обучения)   
 4.6. [*База данных*](#база-данных)   
 4.7. [*Docker и Azure*](#docker-и-azure)   
6. [**Демонстрация решения**](#демонстрация-решения)  
7. [**Направления дальнейшей разработки**](#направления-дальнейшей-разработки)  

**Инструкция по использованию**
===

Инструкция по запуску приложения
---
Cкачать и установить  [FarmingAssistant.apk  ](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/FarmingAssistant.apk)

Запустить и радоваться!

Инструкция по запуску сервера
---
Необходимо запустить контейнер-сервер и открыть порт 80 на используемой машине.  
Запуск существующего образа:  
```
docker pull axhse/farming-assistant-server
docker run -d -p 80:8000 axhse/farming-assistant-server
```
Все ключи доступа и некоторые константы хранятся в переменных окружения.  
Для их быстрой инициализации вы можете использовать .env файл при сборке Docker-образа. Шаблон файла:
[.env.example](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/server/.env.example).  
Вам также потребуется настроить базу данных SQL. Для создания соответствующих таблиц вы можете использовать
[SQL-запрос](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/init_database.sql).

**Описание доменной области**
===

Урожайность посевов сильно зависит от сроков посадки и сбора, поэтому мы разработали мобильное приложение для фермеров, которое поможет снизить потери урожая из-за погодных условий благодаря машиинному обучению.

[Подробное описание доменной области можно прочитать здесь](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/domain.md)

Use Cases 
===
[Про описание Use Cases можно прочитать здесь](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/use-cases.md)

Функциональные и нефункциональные требования 
===
Функциональные:
---
1. **Страница входа** 
   + Вход в созданный ранее аккаунт.
   + Регистрация нового аккаунта с указанием логина, пароля, имени и фамилии (опцинально).
   + Сохранение входа в аккаунт на устройстве.
2. **Главная страница**
   + Рекомендации по срокам посадки, сбора, полива и т. п. для полей пользователя.
   + Возможность оценить рекоменации для конкретного поля.
   + Возможность обновить рекомендации.
3. **Страница погоды**
   + Просмотр прогноза погоды.
4. **Страница “Менеджер  полей”**
   + Добавление нового поля.
   + Просмотр подробной информации о добавленных полях.
   + Редактирование информации о поле.
   + Удаление поля.
5. **Настройки**
   + Выход из аккаунта на устройстве.
   + Переход в этот репозиторий.
   + Отпрака отзыва разработчикам.
6. **Установка приложения выполняется только при соответствии устройства минимальным системным требованиям и наличии необходимого объема свободной памяти.** 



Нефункциональные:
---

1.  Все данные аккаунтов пользователей должны храниться в базе данных.
2.  Серверная часть приложения должна быть оптимизирована для работы с большим количеством пользователей.
3.  Персональная информация пользователей должна передаваться в зашифрованном виде.
4.  Модель машинного обучения использует несколько погодных источников для получения улучшенного прогноза.
5.  Приложение должно запускаться не дольше 3 секунд.
6.  Изменения должны сохраняться в фоновом режиме и не дольше 5 секунд.
7.  Приложение должно сохранять сессию пользователя без необходимости повторной авторизации при загрузке.
8.  Редактирование данных аккаунта должно быть без подключения к интернету, с последующим обновлением при подключении к сети.
9.  Интерфейс мобильного приложения интуитивно понятен и не требует ознакомления с документацией.

Архитектура
===
Первоначальный вариант
---
![Компонентная модель](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_1.jpg)

Компоненты
+ Client
+ Server
  + Controller 
  + Request  Handler
  + AI Module
  + Data  Uploader
+  Database
   + Customer  Database
   + Learning  Database
  
В процессе работы над проектом мы изменили архитектуру в сторону простоты и функциональности. Таким образом она перестала быть перегруженной, но всё ещё полностью отвечает нашим требованиям.

Компонентная модель
--- 
Текущая компонентная диаграмма с учетом используемых технологий:  
![Компонентная модель](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/currentcomponents.png)

Мобильное приложение
--- 
Мобильное приложение было разработано на фреймворк для кроссплатформенной разработки Xamarin с использованием языка C# и среды разработки Visual Studio 2022. Один из разработчиков использовал эмулятор устройства Android, вторая - физический телефон на Android 10. 

Для проектирования был использован паттерн MVVM, так как он соответствует современным стандартам и рекомендуется [Microsoft](https://docs.microsoft.com/ru-ru/xamarin/xamarin-forms/enterprise-application-patterns/mvvm). В нашем приложении View - это страницы на xaml из папки Views, каждая из них привязана к соответствующей ViewModel, в которой и находится логика взаимодействия страницы с внешним миром. Именно из классов-наследников BaseViewModel отправляются запросы на изменение Model. В папке Models можно найти классы аккаунта, вспомогательных объектов и классы отправки запросов на сервер через WebSocket. Экземпляр DataStore из Services нужен для работы с несколькими аккаунтами в приложении.
![MVVM](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_2.jpg). 

Для создания UX/UI-дизайна и разработки макетов мы использовали графический редактор Figma. Одной из наших задач было создание интуитивно понятного и актуального интерфейса. В процессе разработки внешний вид приложения претерпел значительные изменения, а результат Вы можете оценить в разделе демонстрации решения.
![Figma](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/design.png)

Чтобы сохранять вход в учётную запись пользователя на устройстве, мы воспользовались Application.Current.Properties. Этот способ был выбран, так как позволяет не запрашивать доступ к хранилищу данных (а мы все понимаем, как пользователи не любят сомнительных запросов на разрешения приложения). В итоге, Farming Assistant при установке запрашивает только доступ к геолокации для работы с интерактивной картой, это выглядит логично с учётом тематики приложения.

Уже упомянутая интерактивная карта интегрирована в приложение с помощью [Google Maps Api](https://docs.microsoft.com/ru-ru/xamarin/android/platform/maps-and-location/maps/maps-api). Также мы использовали [Acr.UserDialogs nuget](https://www.nuget.org/packages/Acr.UserDialogs/), чтобы добавить симпатичные всплывающие уведомления. Было создано кастомизированное поле ввода, потому что встроенный контрол не соответствовал нашим эстетическим требованиям. Ещё потребовалось разрабатывать свои конвертеры для привязок. 

Сервер
--- 
Сервер реализован на языке Python с использоваением socket.  
Данные аккаунтов пользователей сохраняются в базу данных SQL.  
Для получения погодных данных используются несколько внешних API погоды. 

Модель машинного обучения 
--- 
Если описывать построение “умного прогноза погоды” максимально кратко, то модель строится с помощью catboost. Конечно, можно довериться Яндексу, создавшему catboost, и оставить её составление загадкой, но лучше рассказать подробнее. 

Первое понятие, без которого не обойтись это линейная регрессия. Представим, что у нас есть несколько показателей температур из разных источников и мы хотим строить максимально приближенный к реальности прогноз. Назовём регрессионной моделью сумму исходных числовых показателей, домноженные на какие-то коэффициенты. Тогда для каждого прогноза (с помощью какой-то метрики) можно посчитать величину ошибки, например корень из разности квадратов реальной величины и спрогнозированной. Линейная регрессия — это процесс обучения, когда на каждом шаге коэффициенты регрессионной модели меняются так чтобы среднестатистическая ошибка уменьшилась. Таким образом можно взяв изначально почти случайные коэффициенты получить достаточно хорошо обученную модель. Коэффициенты меняются с помощью частных производных и других понятий из математического анализа, в подробности которого вдаваться не будем (оставим это catboost-у), все что нужно знать, что каждый раз средняя ошибка становиться меньше. 

Но, конечно, это не все, что делает catboost. Чтобы лучше спрогнозировать результат используется Градиентный бустинг. Первое необходимое понятие – дерево решений. Для каждой модели имеется набор значений-признаков и соответствующее дерево, в вершинах дерева — условия. Если условие выполнено, осуществляется переход в правого ребенка вершины, иначе в левого. Нужно пройти до листа по дереву в соответствии со значениями признаков для документа. На выходе каждому документу соответствует значение листа. Это и есть ответ. Теперь объясним, что такое Градиентный бустинг. Идея бустинг-подхода нам уже знакома: для слабых функций, которые строятся в ходе итеративного процесса, на каждом шаге новая модель обучается с помощью данных об ошибках предыдущих. Результирующая функция представляет собой линейную комбинацию базовых, слабых моделей. Теперь используем только что определённые деревья. Будем строить несколько деревьев, чтобы добавление новых деревьев уменьшало ошибку. Итого при достаточно большом количестве деревьев мы сможем сильно уменьшить ошибку относительно простой регрессии. К сожалению, в нашем случае ошибка уменьшилась всего на 10 процентов, но все же. 

И так как существует еще миллион нюансов, то гораздо эффективней не писать это самому, а использовать созданный и выложенной в open source Яндексом catboost. Более подробно про его устройство можно почитать [тут](https://catboost.ai/en/docs/) 

Отдельный вопрос о том, как оценивать ошибку. Так как для построения, скажем прогнозируемой температуры, использовались только соответствующие показатели из оригинальных источников, то за метрику была выбрана довольна простая метрика MSE, то есть просто среднеквадратичная ошибка. 

База данных
--- 

База данных (SQL) содержит две таблицы.  
Строки таблицы customer являются группой из username пользователя, данных его аккаунта и хеша пароля.  
Таблица auth_token хратит токены, использующиеся для аутентификации пользователей в привязке с username.  
При создании таблиц типы данных всех столбцов определялись с учетом реально допустимых значений хранимых данных.  

Пример содержимого базы данных клиентов:  
![](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/customer_db.png)

Docker и Azure
---
Северная часть приложения упакована в Docker. [Актуальный образ](https://hub.docker.com/repository/docker/axhse/farming-assistant-server).  
Для сборки используется [данный Dockerfile](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/server/Dockerfile).  

Контейнер-сервер запущен на виртуальной машине Azure и взаимодействует с базой данных Azure SQL по виртуальной сети.  

Демонстрация решения
=== 

![SignUp](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/signup.jpg)

![SignIn](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/signin.jpg)

![MainPge](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/mainpage.jpg)

![WeatherPage](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/weather.png)

![FieldsPage](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/fields.jpg)

![SettingsPage](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/settings.jpg)

Направления дальнейшей разработки
=== 

Как мы видели наш путь на НИСе в начале года
![Roadmap](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/roadmap1.png)

Как наш проект может разиваться в будущем
![Roadmap2](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Roadmap.png)

Планы по улучшению MVP  
+ Сделать приложение и сервер многопоточными 

+ Добавить оформление платной подписки 

+ Расширить перечень доступных культур 

+ Добавить регистрацию и авторизацию через соцсети 
