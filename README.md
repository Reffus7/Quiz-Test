# Quiz Test

Данный проект основан на тестовом задании для демонстрации навыков.   
Игра предназначена для проверки внимательности: игрок должен выбрать правильный символ (цифру или букву) среди предложенных вариантов.

## Функционал

### Игровой процесс
- В игре три уровня сложности:
  - Легкий – 3 ячейки
  - Средний – 6 ячеек
  - Сложный – 9 ячеек
- Игрок начинает с легкого уровня и последовательно переходит к более сложным.
- После завершения последнего уровня появляется кнопка Restart, позволяющая начать игру заново.

### Генерация данных
- В начале каждого уровня случайным образом выбирается цель задания (например, найти "5" или "F").
- Один и тот же правильный ответ не повторяется в рамках одной игровой сессии.
- Используются два набора данных: цифры (1-9) и буквы (A-Z).
- Код не привязан к конкретному типу данных – игра должна поддерживать любые другие наборы (например, фрукты, машины) без изменения логики.

## Визуальные эффекты и анимации

Необходимы анимации для следующих случаев:
- Появление ячеек
- Появление текста задания
- Нажатие на неправильный ответ
- Нажатие на правильный ответ с появлением частиц в виде звездочек в области правильного ответа
- Затемнение экрана при завершении игры
- Загрузочный экран при перезапуске игры

## Технические требования

### Архитектура и код
- Принципы SOLID
- Отсутствие классов, отвечающих за всю игру целиком
- Разделение логики (отдельные классы для генерации уровней, анимаций, UI, загрузки данных и т. д.)
- Инкапсуляция – никаких публичных полей без необходимости
- Гибкая генерация сетки – количество ячеек и их расположение задается через ScriptableObject, а не жестко в коде
- Оптимизация – исключены дорогостоящие операции вроде .Find, .GetComponent в Update() и т. д.
- Структура данных построена на ScriptableObject:
  - Позволяет легко менять набор данных (цифры, буквы, изображения и т. д.)
  - Обеспечивает гибкость и возможность расширения

### Используемые технологии
- DOTween (анимации: скейл, fade, движение)
- Zenject (внедрение зависимостей, DI)

### Ограничения
- Запрещено использовать сторонние библиотеки (кроме DOTween и Zenject)
- Запрещено использование static-полей (кроме методов-расширений)
- Запрещено использовать Singleton
- Запрещено привязывать сложность уровней к фиксированным значениям (например, не должно быть enum Difficulty, +=3 для ячеек)

### Стилистика кода
- Код структурирован, придерживается единого стиля, отсутствуют лишние комментарии и пустые методы
- Используется namespace
- Наименования класса, поля, метода должны отражать суть
- Модификаторы доступа указаны в явном виде

## Дополнительно от ТЗ
- Добавлены звуки при выборе ответа
- Используется Object Pool для ячеек
- Обрабатываются ошибки при нехватке спрайтов
- Добавлен 4 уровень с большим количеством ячеек
