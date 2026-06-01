# Лабораторная работа по веб-технологиям

Лабораторная работа на ASP.NET Core: общий REST API каталога книг и два клиентских приложения (MVC и Blazor).

## Состав решения

| Проект | Описание |
|--------|----------|
| **Ignatovich.Domain** | Общие сущности и модели (`Book`, `Author`, `ResponseData`) |
| **Ignatovich.API** | REST API (SQLite, CRUD для книг и авторов) |
| **Ignatovich.UI** | Клиент на ASP.NET Core MVC |
| **Ignatovich.Blazor** | Клиент на Blazor Server |

## Запуск локально

1. Запустите API: `Ignatovich.API` — https://localhost:7281  
2. Запустите нужный клиент:
   - `Ignatovich.UI` — https://localhost:7001  
   - `Ignatovich.Blazor` — https://localhost:7220

Приложения UI и Blazor обращаются к API по HTTPS. Сначала запускайте API.

## Скриншоты

### Ignatovich.Blazor

![Blazor — каталог книг](https://raw.githubusercontent.com/isyzes/Web-technologies-laboratory/refs/heads/main/Ignatovich.API/ui-images/blazor-1.PNG)

![Blazor — детали книги](https://raw.githubusercontent.com/isyzes/Web-technologies-laboratory/refs/heads/main/Ignatovich.API/ui-images/blazor-2.PNG)

### Ignatovich.UI

![UI — главная](https://raw.githubusercontent.com/isyzes/Web-technologies-laboratory/refs/heads/main/Ignatovich.API/ui-images/ui-1.PNG)

![UI — каталог](https://raw.githubusercontent.com/isyzes/Web-technologies-laboratory/refs/heads/main/Ignatovich.API/ui-images/ui-2.PNG)

![UI — область администрирования](https://raw.githubusercontent.com/isyzes/Web-technologies-laboratory/refs/heads/main/Ignatovich.API/ui-images/ui-3.PNG)
