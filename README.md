# sysABC
It's RESTful web API, it's provide:
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

Orginal task:
2. RESTful API (Python/C# lub Java):
   - Umozliwiajace operacje na liscie uzytkownikow systemu ABC.
      1.   Autoryzjacja/uwierzytelnianie
      2.  Tworzenie kont uzytkownikow
      3. Przegladanie listy uzytkownikow
      4.  *) nadawianie ról
   - *) backend w postaci SQLLite DB
   - *) Instrukcja włączenia w języku angielskim"