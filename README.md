# Autentikaatio & Autorisaatio Demo

ASP.NET Core 8.0 -sovellus, joka demonstroi JWT-pohjaista autentikointia ja autorisointia.

## Ominaisuudet

- **JWT-autentikaatio**: Käyttäjät voivat kirjautua sisään ja saada JWT-tokenin
- **Endpointtien suojaus**: [Authorize]-attribuutin avulla suojatut ja avoimet endpointit
- **Login-palvelu**: Demonstratiivinen login-endpointti, joka tarkistaa käyttäjätiedot ja palauttaa tokenin
- **TokenService**: JWT-tokenien generointia varten

## API-endpointit

### POST /weatherforecast/login
Kirjautuminen ja tokenin hakeminen.

```json
{
  "username": "testuser", 
  "password": "testpassword"
}
tai
{
  "username": "adminuser", 
  "password": "adminpassword"
}
```

**Vastaus:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

### GET /weatherforecast/OpenGet
Avoin endpointti (ei autentikointia vaadita).

### GET /weatherforecast/AuthGet
Suojattu endpointti (vaatii validan JWT-tokenin Authorization-headerissa).

```
Authorization: Bearer <token>
```

## Asennus ja käynnistys

```bash
# Asenna riippuvuudet
dotnet restore

# Käynnistä sovellus
dotnet run
```

Sovellus käynnistyy osoitteessa `http://localhost:5285`

## Testikyselyt

Swagger UI on saatavilla osoitteessa `http://localhost:5285/swagger`
