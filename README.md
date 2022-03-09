﻿# Содержание

 1. [**Описание доменной области**](#описание-доменной-области)  
 1.1.[*Коротко о нашем проекте*](#коротко-о-нашем-проекте)  
 1.2. [*Введение*](#а.-введение)  
 1.3. [*Клиенты и пользователи*](#б.-клиенты-и-пользователи)  
 1.4. [*Окружение*](#в.-окружение)  
1.5. [*Как происходит прогнозирование погодных данных в настоящее время?*](#г.-как-происходит-прогнозирование-погодных-данных-в-настоящее-время)  
1.6. [*Конкурирующее программное обеспечение* ](#д.-конкурирующее-программное-обеспечение )  
 1.7. [*Общие знания о сфере*](#е.-общие-знания-о-сфере)  
 2. [**Use Cases**](#use-cases)  
 2.1. [*Категории юзкейсов*](#категории-юзкейсов)  
 2.2. [*Запуск системы*](#запуск-системы)  
 2.3. [*Регистрация*](#регистрация)  
 2.4. [*Авторизация*](#авторизация)  
 2.5. [*Добавление поля*](#добавление-поля)  
 2.6. [*Получение рекомендаций по культурам для посадки*](#получение-рекомендаций-по-культурам-для-посадки)  
 2.7. [*Получение рекомендаций по срокам сбора урожая*](#получение-рекомендаций-по-срокам-сбора-урожая)  
  3. [**Функциональные и нефункциональные требования**](#функциональные-и-нефункциональные-требования)  
  3.1. [*Функциональные*](#функциональные)  
  3.2. [*Нефункциональные*](#нефункциональные)  
  4. [**Компонентная модель**](#компонентная-модель)  
  5. [**Референсные архитектуры**](#референсные-архитектуры)
  6. [**Docker**](#docker)  

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

3. Пользователь вводит почту (она же логин) и нажимает кнопку “подтвердить почту”. 

4. Система высылает письмо с подтверждением на почту. 

5. Пользователь подтверждает почту. 

6. Система получает подтверждение и активирует поля с паролем. 

7. Пользователь вводит пароль и повтор пароля и нажимает “зарегистрироваться”. 

8. Система создаёт аккаунт пользователя и даёт разрешение на вход и выводит сообщение об успешной регистрации. 

**Альтернативные сценарии:**  

**(3)** *Если уже существует аккаунт с такой почтой, то система выводит сообщение об  этом.* 

**(7.1)** *Если пароли не совпадают или не соответствуют требованиям надёжности, то система выводит сообщение об ошибке.* 

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

4. (опционально) Пользователь указывает дату посадки культуры. 

5. (опционально) Пользователь указывает размер поля. 

6. Система сохраняет информацию о поле на устройстве пользователя. 

7. Система сохраняет ассоциированную с аккаунтом пользователя информацию о поле на сервере. 

8. Система выводит сообщение об успешном добавлении поля. 

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

4. (опционально) Отображается время последнего обновления рекомендации, если оно происходило более 10 минут назад. 

5. Пользователь переходит к шагу 1 или выходит из раздела. 

Функциональные и нефункциональные требования 
===
**(1)** Функциональные:
---
1.  **Страница входа**  
    + Возможность войти в созданный ранее учетную запись.
    + Возможность создать новый аккаунт с указанием логина, пароля, местоположения, имени и адреса электронной почты.
7. **Страница “Менеджер подписки”**
    + Активация подписки на пробный период. Доступно только один раз для нового пользователя.
    + Подключение платной подписки.
    + Продление платной подписки.
8. **Страница “Менеджер  полей”**
    + Добавление нового поля. (Выбор размеров, культуры, ключевых цифр и дат).
    + Редактирование информации о поле.
    + Удаление поля.
    + Совет по выбору/изменению культуры для каждого поля.
    + Рекомендации по срокам посадки, сбора, полива и т. п. для выбранного поля
9. **Главная страница**
   + Уведомления о приближении сроков рекомендаций по уборке, посадке, поливу и т. п.
   + Выбор временного периода.
   + Возможность подтвердить данные прогноза на данный момент.
   + Индивидуальный прогноз погоды на выбранный период.
   + Переход на страницы “Менеджер подписки” и входа.
5. **Установка приложения выполняется только при соответствии устройства минимальным системным требованиям и наличии необходимого объема свободной памяти.** 



**(2)** Нефункциональные:
---
1.  Приложение должно поддерживать работу с мобильных устройств (соответствующих минимальным требованиям).
    

2.  Объём используемой оперативной памяти не должен превышать 512 Мб .
    

3.  Данные передаются в зашифрованном виде.
    

4.  Все данные о пользователях системы должны храниться в БД.
    

5.  Для ускорения поиска данных по определенному пользователю должны быть предусмотрены индексы по соответствующим полям таблицы.
    

6.  Система должна обрабатывать 10 000 и меньше пользователей без какого-либо снижения производительности
    

7.  Главное окно не должно загружаться более 2 секунд.
    

8.  Окно погоды не должно загружаться более 30 секунд.
    

9.  При аутентификации пользователя в информационной системе должна использоваться только идентификация по паролю.
    

10.  Новая учетная запись пользователя будет должна создаваться менее чем за 1 с.
    

11.  Индивидуальный прогноз погоды получаются на основе анализа нейронной сетью данных о погоде с нескольких источников и постанализа в независимых источниках
    

12.  Приложение дает пользователю доступ к последним рекомендациям без подключения к интернету.
    

13.  Интерфейс как мобильного приложения интуитивно понятен и не требует ознакомления с документацией.
    

14.  Все исходные файлы должны быть документированы.

Компонентная модель
=== 
![Компонентная модель](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_1.jpg)
---
Компоненты
---
+ Client
+ Server
  + Controller 
  + Request  Handler
  + AI Module
  + Data  Uploader
+  Database
   + Customer  Database
   + Learning  Database
  
Client
---
Клиентская часть приложения будет разработана с использованием технологии Xamarin  Forms, которая была выбрана благодаря её кроссплатформенности. При проектировании будет применяться шаблон MVVM, так как он соответствует современным стандартам и рекомендуется [Microsoft](https://docs.microsoft.com/ru-ru/xamarin/xamarin-forms/enterprise-application-patterns/mvvm).

![.](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_2.jpg)

Server
---
1. Управляющий сервер задает параметры работы других серверов и перезапускает их в случае сбоя.
2. Загрузчик данных обновляет базу с данными для обучения, а также реализует публичные методы для получения текущих погодных данных.
3. Модуль ИИ обучает нейросеть, а также реализует публичные методы для получения рекомендаций.
4. Обработчик запросов обрабатывает запросы пользователей приложения.

Data Base
---
1. Пользовательская база данных 
2. База данных для обучения 

AI Module
---
Существует некоторое число источников данных, которые для одного и того же места и времени различны. То есть разные источники могут давать разные предсказания температуры, влажности и прочих погодных условия. Для решения этой проблемы будет создана нейронная сеть. Для нахождения более верного значения каждого из параметров будет реализована нейронная сеть прямого распространения. Для обучения будут использоваться погодные данные источников для различных местоположений за последние несколько лет. Для определения ошибки будет использоваться не только разность действительной величины от предсказанной, но и другие факторы такие как местоположение объекта, тенденции изменения погодных данных и другие, взятые с различными весами. Таким образом нейронная сеть поможет сгенерировать более достоверный прогноз погоды для конкретного пользователя. После запуска приложения нейросеть продолжит улучшение основываясь на пользовательских подтверждениях или опровержениях предоставленного прогноза. Таким образом качество прогнозов будет лучше, а их эффективность будет объективно оцениваться.

Референсные архитектуры
===
В качестве референсных архитектур мы решили взять Data discovery  reference  architecture от IBM и Мобильное клиентское приложение на основе задач от Microsoft

На первую мы будем ориентироваться при проектировании сбора данных о прогнозе погоды (элементы в DATA COLLECTION RUNTIME FLOW). Из второй архитектуры мы взяли то, как сервисы Azure встраиваются в разработку ПО.

![enter image description here](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_3.jpg)
![enter image description here](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_4.png)

Docker
===
Для создания инфраструктуры мы будем использовать Docker. 

На данном этапе планируется 5 контейнеров в соответствии с архитектурой.

![enter image description here](https://github.com/Alena-Vasileva/FarmerCyberAssistant/blob/main/img/Image_5.jpg)


