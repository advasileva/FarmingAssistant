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

Е. Общие знания о сфере. 
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

**(5)** Получение рекомендаций по культурам для посадки.
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

**(6)** Получение рекомендаций по срокам сбора урожа.
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
1. Возможность создания аккаунта. Проверка корректности электронной почты и подтверждение владения через электронное письмо. Авторизация в созданный аккаунт. 

2. Менеджер подписки: активация пробного периода, подключение подписки, продление подписки 

3. Менеджер полей: просмотр поля, создание поля, редактирование поля, удаление поля. 

4.  Выбор оптимальных культур для выбранного пользователем поля  

5. Сбор данных о погоде с нескольких источников со свободным доступом 

6. Анализ нейронной сетью данных о погоде с нескольких источников и получение индивидуального прогноза погоды для каждого пользователя. 

7. Составление рекомендаций по срокам посадки и сбора (полива и обработки удобрениями) для выбранного поля 

8. Подключение клиента к серверу 

9. Постанализ прогноза погоды в независимых источниках 

10. Получение обратной реакции от пользователя 

11. Сохранение последних полученных рекомендаций для ответов на запросы пользователя без доступа в интернет. 

12. Установка приложения выполняется только при соответствии устройства минимальным системным требованиям и наличии необходимого объема свободной памяти. 

13. Данные о погоде предоставляться в объёме более 1 тысячи одновременных запросов без снижения производительности с загрузкой пользователю не более 3 секунд. 


**(2)** Нефункциональные:
---
1. Все функции сервиса доступны в мобильном кроссплатформенном приложении. 

2. Интерфейс как мобильного приложения интуитивно понятен и не требует ознакомления с документацией  

3. Система не должна завершаться аварийно при любых действиях пользователя. 

4. Все исходные файлы должны быть документированы. 

5. Полная конфиденциальность. Отсутствие доступа к данным аккаунтов для третьих лиц. 

6. Поддержка только русского языка 
