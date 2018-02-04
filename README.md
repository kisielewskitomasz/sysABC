# sysABC
It's RESTful web API, it's provide:
- authorization via JWT
- register new user
- browse all users
- browse single user
- change user role (requires admin role)
- delete user (requires admin role)
- get admin JWT (for testing and develop)

Postman's queries: https://documenter.getpostman.com/view/3607260/sysabc/7TT7pZC

# Description:

- authorization via JWT:
POST http://localhost:5000/api/users/token
with json:
{
  "email": "username",
  "password": "password"
}

- register new user
POST http://localhost:5000/api/users/register
with json:
{
  "email": "email",
  "password": "password",
  "nickName": "usernick",
  "firstName": "firstname",
  "lastName": "lastname"
}

- browse all users
GET http://localhost:5000/api/users/

- browse single user
GET http://localhost:5000/api/users/put_user_mail_here

- change user role (requires admin role)
PUT http://localhost:5000/api/users/register
Header: Authorization adminJWT
with json:
{
  "email": "email",
  "role": "role",
}

- delete user (requires admin role)
DELETE http://localhost:5000/api/users/put_user_mail_here
Header: Authorization adminJWT

- get admin JWT (for testing and develop)
GET http://localhost:5000/api/users/token/admin

# Orginal task:
RESTful API (Python/C# lub Java):
   - Umozliwiajace operacje na liscie uzytkownikow systemu ABC.
      1.   Autoryzjacja/uwierzytelnianie
      2.  Tworzenie kont uzytkownikow
      3. Przegladanie listy uzytkownikow
      4.  *) nadawianie ról
   - *) backend w postaci SQLLite DB
   - *) Instrukcja włączenia w języku angielskim
