﻿# Содержание

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
4. [**Архитектура**](#архитектура)  
 4.1. [*Первоначальный вариант*](#первоначальный-вариант)  
 4.2. [*Компонентная модель*](#компонентная-модель)  
 4.3. [*Мобильное приложение*](#мобильное-приложение)   
 4.4. [*Сервер*](#сервер)   
 4.5. [*Модель машинного обучения*](#модель-машинного-обучения)   
 4.6. [*База данных*](#база-данных)   
 4.7. [*Docker и Azure*](#docker-и-azure)   
 4.8. [*Инструкция по запуску сервера*](#инструкция-по-запуску-сервера)  
5. [**Демонстрация решения**](#демонстрация-решения)  
6. [**Направления дальнейшей разработки**](#направления-дальнейшей-разработки)  

**Описание доменной области**
===
Коротко о нашем проекте:
---
  Из года в год фермеры по всему миру сталкиваются с потерей урожая, потому что в современном сельском хозяйстве всё ещё не хватает качественного анализа изменений погодных условий. Локально эта проблема выражается в невозможности эффективно составить график сельскохозяйственных работ. Цель создаваемого программного обеспечения – с математической точностью указать фермеру на лучшие сроки сбора и посадки урожая 

Используя это приложение, фермер заполняет сведения о поле: геолокация, культура, опционально этап созревания и особенности. Далее нейросеть анализирует полученные данные, а также прогнозы погодных условий и даёт рекомендации фермеру по посадке / сбору урожая. При существенном изменении прогнозов погоды фермер получает новые рекомендации. 

Фермеры ценят возможность работы без подключения к интернету, поэтому нашим клиентам будет предоставлено автономное приложение с загрузкой актуальных данных из облака. Доступ к этому сервису будет осуществляться по подписке. Новым пользователям будет предоставляться бесплатная пробная версия в течение двух недель для ознакомления с функционалом.  

А. Введение
---
В этом документе описывается исходная информация, которая была собрана о фермерстве в Российской Федерации. Эта информация будет использоваться при разработке программного обеспечения для анализа погодных данных с помощью нейросети. 

Б. Клиенты и пользователи 
---
Наши потенциальные клиенты - малые и средние фермерские хозяйства, специализирующиеся на растениеводстве. Всего в России более 176 тыс. крестьянских и фермерских хозяйств, 26 тыс. микропредприятий, 32 тыс. малых сельхоз организаций. В отличие от крупных компаний они не могут позволить себе установку собственных метеостанций, поэтому нуждаются в альтернативном решении. К тому же фермерские хозяйства в нашей стране сейчас активно развиваются.   

В. Окружение 
---

Пользователь будет взаимодействовать с нашим сервисом через мобильное приложение, потому что это вариант наиболее подходит под наши цели и обладает следующими преимуществами: 

1) Мобильное устройство с возможностью устанавливать приложения есть у подавляющего большинства фермеров. 

2) Мобильное устройство всегда “под рукой”, а во время работ на ферме и вовсе может являться единственным источником информации. 

3) В отличие от веб-сервисов, в мобильное приложение можно загрузить результаты, чтобы фермер мог получить к ним доступ даже без подключения к интернету и обновить данные при возможности. Очевидно, что далеко не на всех фермах есть доступ в сеть, поэтому мы считаем это главным преимуществом. 

4) Возможность присылать пуш-уведовления пользователю при значительных изменениях в рекомендациях.  

Кроме того, мы не исключаем появление веб-сервиса и десктопного приложения в будущем.  

Г. Как происходит прогнозирование погодных данных в настоящее время?  
---
1) Фермер смотрит прогнозы погоды в различных источниках, пытаясь составить целостную картину, чтобы постараться верно предугадать погодные условия и не потерять значительную часть своего урожая.   

2) Фермер находит компанию, которая устанавливает автономные метеостанции, платит им за станцию, её установку, а затем ежегодно оплачивает дорогостоящее обслуживание.  

Как мы видим, (1) способом пользуются начинающие фермеры с небольшим хозяйством. У них нет денег, чтобы оплачивать дорогостоящее оборудование, поэтому они стараются сделать всё самостоятельно. Их прогнозирование больше напоминает игру “Орёл – решка”, причём монетки они подкидывают не один раз в год. Соответственно, чем больше бросков – тем меньше шанс угадать.  

Способ (2) выбирают крупные и состоявшиеся компании, у которых есть бюджет для установки подобных метеостанций. Они хотят повысить кол-во собранного урожая, повысив точность анализа погодных условий. Безусловно, компактные метеостанции – достаточно точно прогнозируют предстоящие погодные условия, но из-за своей стоимости подходят не каждому фермеру.  
 

Д. Конкурирующее программное обеспечение  
---
Мы познакомились с конкурирующими решениями и выделили 3 типа конкурентов: прямые, непрямые и косвенные.  

К первому типу относятся комплексные приложения для фермеров с встроенным прогнозом погоды (Н: Сингента Россия) и компактные метеостанции, о которых сказано в пункте (Г).  От Сингенты же мы отличаемся тем, что делаем специализированные прогнозы, которые подстраиваются под нужды фермеров. 

К непрямым конкурентам относятся прогнозы погоды, такие как Gismeteo и Wunderground и приложение для индийских фермеров – AI Sowing, в котором искусственный интеллект даёт рекомендации на основе погодных данных. Однако Gismeteo и Wunderground не дают специализированных прогнозов для фермеров, а AI Sowing не специализируется на российском рынке. 

Нашим косвенным конкурентов являются приложения для борьбы с сорняками и вредителями (Н: ID Weeds и Fertilizer Removal). Они так же, как и мы дают рекомендации фермерам, но совсем в другой области. Данные приложения не специализируются на прогнозах погоды.  

Е. Общие знания о сфере
---
1) Спрос на продовольственные товары растёт, а расширение посевных площадей не может продолжаться вечно, поэтому требуются инновационные решения для повышения качества сбора и посадки урожая.  

2) Законодательная база. В данных момент в государственной думе на рассмотрении законопроект о сбыте произведённой продукции фермерскими хозяйствами. 

3) Мы проанализировали рынок сельского хозяйства. Наша ниша - сокращение потерь урожая из-за непогоды. По данным Правительства РФ прибыль фермерских хозяйств в России до налогообложения в 2020 году составила 624,2 млрд рублей. Так же Росбалт сообщает, что в 2020 году из-за погодных условий аграрии потеряли более 7.5 млрд рублей. Эти 7,5 млрд рублей и есть потенциальная ёмкость рынка в нашей области. 

Use Cases 
===
Категории юзкейсов 
---
**Аккаунт:** регистрация и авторизация  

**Менеджер подписки:** активация пробного периода, подключение подписки, продление подписки 

**Менеджер полей:** просмотр поля, создание поля, редактирование поля, удаление поля 

**Выбор оптимальных культур** 

**Сроки посадки и сбора** 

**Полив и удобрения**

**(1)** Запуск системы
---
**Название:** Запуск системы 

**Описание:** Пользователь входит в систему, чтобы получить доступ к ее функционалу. 

**Предусловия:** Приложение установлено. 

**Результат:** Система готова к использованию. 

**Триггер:** Пользователь открывает приложение. 

**Успешный сценарий:**

1. Система выводит приветственное окно и загружается. 

2. Если на устройстве уже была выполнена авторизация, то система готова к использованию и процесс завершается. 

3. Система выводит форму входа. Далее возможны сценарии “регистрация” и “авторизация”. 

**Альтернативные сценарии:** - 

**(2)** Регистрация 
---
**Название:** Регистрация 

**Описание:** Пользователь создаёт аккаунт в системе. 

**Предусловия:** Система должна быть подключена к сети Интернет. 

**Результат:** Пользователь зарегистрирован. 

**Триггер:** - 

**Успешный сценарий:** 

1. Пользователь нажимает на кнопку “регистрация”. 

2. Система выводит форму регистрации. 

3. Пользователь вводит логин и пароль и нажимает “зарегистрироваться”. 

4. Система создаёт аккаунт пользователя и даёт разрешение на вход и выводит сообщение об успешной регистрации. 

**Альтернативные сценарии:**  

**(4)** *Если уже существует аккаунт с таким логином, то система выводит сообщение об  этом.* 

**(3)** Авторизация 
---
**Название:** Авторизация 

**Описание:** Пользователь авторизуется в системе. 

**Предусловия:** Система должна быть подключена к сети Интернет. 

**Результат:** Пользователь авторизован. 

**Триггер:** Система не обнаружила выполненных авторизаций на устройстве. 

**Успешный сценарий:** 

1. Пользователь вводит логин и пароль и наживает на кнопку “вход”. 

2. Система проверяет логин и пароль. 

3. Система даёт разрешение на вход и выводит сообщение об успешной авторизации. 

**Альтернативные сценарии:**  

**(2.1)** *Если одно из полей не заполнено, то система выводит сообщение об ошибке в незаполненном поле.* 

**(3.1)** *Если логин не найден, то система выводит сообщение об ошибке в логине.* 

**(3.2)** *Если пароль не верен, то система выводит сообщение об ошибке в пароле.*

**(4)** Добавление поля 
--- 
**Название:** Добавление поля 

**Цель:** Получение от пользователя и сохранение информации о новом поле: геолокация, культура, опционально этап созревания и особенности. 

**Предусловия:** Пользователь должен быть авторизован, устройство должно быть подключено к интернету. 

**Результат:** Информация о новом поле сохранена на сервере и локально на устройстве пользователя. 

**Триггер:** *Пользователь переходит в меню добавления поля.* 

**Успешный сценарий:** 

1. Пользователь указывает название поля. 

2. Пользователь указывает координаты поля с помощью интерактивной карты. 

3. (опционально) Пользователь выбирает выращиваемую на данном поле культуру из выпадающего списка. 

4. Система сохраняет информацию о поле на устройстве пользователя. 

5. Система сохраняет ассоциированную с аккаунтом пользователя информацию о поле на сервере. 

6. Система выводит сообщение об успешном добавлении поля. 

**(5)** Получение рекомендаций по культурам для посадки
---
**Название:** получение рекомендаций по культурам для посадки. 

**Цель:** Получение пользователем информации об оптимальных культурах для посадки. 

**Предусловия:** Пользователь должен быть авторизован и иметь действующую подписку. 

**Результат:** Пользователь получил рекомендации. 

**Триггер:** *Пользователь переходит в раздел рекомендаций “Оптимальные культуры”.* 
 
 **Успешный сценарий:** 

1.  Пользователь выбирает одно из добавленных полей из выпадающего списка. 

2. Если устройство подключено к интернету, система загружает рекомендацию с сервера и сохраняет на устройстве. 

3. Система показывает рекомендацию на той же странице. *(Если рекомендация не сохранена на устройстве, выводится сообщение о том, что рекомендацию невозможно получить)*

4. (опционально) Отображается время последнего обновления рекомендации, если оно происходило более 10 минут назад. 

5. Пользователь переходит к шагу 1 или выходит из раздела. 

**(6)** Получение рекомендаций по срокам сбора урожая
---
**Название:** получение рекомендаций по срокам сбора урожая. 

**Цель:** Получение пользователем информации об оптимальных сроках сбора урожая. 

**Предусловия:** Пользователь должен быть авторизован и иметь действующую подписку. 

**Результат:** Пользователь получил рекомендации. 

**Триггер:** *Пользователь переходит в раздел рекомендаций “Оптимальные сроки сбора урожая”.* 
 
 **Успешный сценарий:** 

1.  Пользователь выбирает одно из добавленных полей из выпадающего списка. 

2. Если устройство подключено к интернету, система загружает рекомендацию с сервера и сохраняет на устройстве. 

3. Система показывает рекомендацию на той же странице. *(Если рекомендация не сохранена на устройстве, выводится сообщение о том, что рекомендацию невозможно получить)*

4. Пользователь переходит к шагу 1 или выходит из раздела. 

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
4. **Страница “Менеджер  полей”** Сделаны в соответсвии с CRUD
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
**МАТЕРИАЛ ПО ФИГМЕ**

Чтобы сохранять вход в учётную запись пользователя на устройстве, мы воспользовались Application.Current.Properties. Этот способ был выбран, так как позволяет не запрашивать доступ к хранилищу данных (а мы все понимаем, как пользователи не любят сомнительных запросов на разрешения приложения). В итоге, Farming Assistant при установке запрашивает только доступ к геолокации для работы с интерактивной картой, это выглядит логично с учётом тематики приложения.

Уже упомянутая интерактивная карта интегрирована в приложение с помощью [Google Maps Api](https://docs.microsoft.com/ru-ru/xamarin/android/platform/maps-and-location/maps/maps-api). Также мы использовали [Acr.UserDialogs nuget](https://www.nuget.org/packages/Acr.UserDialogs/), чтобы добавить симпатичные всплывающие уведомления. Было создано кастомизированное поле ввода, потому что встроенный контрол не соответствовал нашим эстетическим требованиям. Ещё потребовалось разрабатывать свои конвертеры для привязок. 

Сервер
--- 
Сервер реализован на языке Python с использоваением socket.  
Данные аккаунтов пользователей сохраняются в базу данных SQL.  
Для получения погодных данных используются несколько внешних API погоды. 

Модель машинного обучения 
--- 
Если описывать построение “умного прогноза погоды” максимально кратко, то модель строиться с помощью catboost. Конечно, можно довериться Яндексу, создавшему catboost, и оставить её составление загадкой, но лучше рассказать подробнее. 

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

**TO DO КАРТИНКИ ИЗ ПРЕЗЕНТАЦИИ**

Docker и Azure
---
Северная часть приложения упакована в Docker. [Актуальный образ](https://hub.docker.com/repository/docker/axhse/farming-assistant-server).  
Для сборки используется [данный Dockerfile](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/server/Dockerfile).  

Контейнер-сервер запущен на виртуальной машине Azure и взаимодействует с базой данных Azure SQL по виртуальной сети.  

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

Демонстрация решения
=== 

Направления дальнейшей разработки
=== 

**РОАДМАПЫ**
