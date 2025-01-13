# Account Microservice

## 📝 Opis projektu
Mikroserwis Account jest częścią większego systemu kasyna online, odpowiedzialny za zarządzanie kontami użytkowników oraz ich sesjami.

Serwis jest jednym z czterech mikroserwisów tworzących kompletny system:
- 🎮 Game Service - obsługa logiki gier
- 👤 Account Service (ten projekt) - zarządzanie użytkownikami i sesjami
- 💰 Payment Service - obsługa płatności
- 🔀 Gateway Service - zarządzanie komunikacją między serwisami

## 🛠 Technologie
- ASP.NET Core
- Entity Framework Core
- MS SQL Server
- Docker
- REST API

## 🔒 Zabezpieczenia
System implementuje wielopoziomowe zabezpieczenia:
1. **IP Whitelist**
   - Filtrowanie requestów na podstawie dozwolonych adresów IP
   - Konfiguracja w pliku appsettings.json

2. **Custom Header Validation**
   - Walidacja specjalnego nagłówka w każdym żądaniu
   - Wartość nagłówka porównywana z konfiguracją w appsettings.json

## 📋 Wymagania systemowe
- .NET 6.0 lub nowszy
- Docker Desktop
- MS SQL Server (opcjonalnie, jeśli nie używamy Dockera)

## ⚙️ Konfiguracja i uruchomienie

### Przy użyciu Dockera:
```bash
# Sklonuj repozytorium
git clone https://github.com/IgorTomasz/account-service.git

# Przejdź do katalogu projektu
cd account_service

# Zbuduj i uruchom kontenery
docker-compose up --build
```

### Lokalne uruchomienie:
1. Sklonuj repozytorium
2. Zaktualizuj connection string w `appsettings.json`
3. Wykonaj migracje bazy danych:
```bash
dotnet ef database update
```
4. Uruchom aplikację:
```bash
dotnet run
```

## 🚀 Endpointy API

### Zarządzanie Użytkownikami (UserController)

#### Autentykacja
```http
# Logowanie użytkownika
POST /account/user/auth/login
```
Request body:
```json
{
    "username": "string",
    "password": "string"
}
```

```http
# Rejestracja nowego użytkownika
POST /account/user/auth/register
```
Request body:
```json
{
    "username": "string",
    "password": "string",
    "email": "string",
    "name": "string",
    "lastname": "string",
    "dateOfBirth": "string (format: YYYY-MM-DD)"
}
```

#### Zarządzanie profilem
```http
# Pobranie profilu użytkownika
GET /account/user/profile/{userId}

# Aktualizacja hasła
PATCH /account/user/profile/update-password
```
Request body dla zmiany hasła:
```json
{
    "userId": "guid",
    "oldPassword": "string",
    "newPassword": "string"
}
```

#### Panel Administracyjny
```http
# Pobranie wszystkich użytkowników
GET /account/user/adm/users

# Usunięcie użytkownika
DELETE /account/user/adm/delete?userId={guid}
```

### Zarządzanie Sesjami (UserSessionController)

#### Operacje na sesjach
```http
# Utworzenie nowej sesji
POST /account/usersession/auth/session/create
```
Request body:
```json
{
    "userId": "guid",
    "deviceInfo": "string",
    "ipAddress": "string"
}
```

```http
# Pobranie wszystkich sesji
GET /account/usersession/auth/session/all

# Pobranie konkretnej sesji
GET /account/usersession/auth/session/{sessionId}

# Wylogowanie (zakończenie sesji)
PATCH /account/usersession/auth/session/logout/{sessionId}

# Aktualizacja sesji
PATCH /account/usersession/auth/session/update
```
Request body dla aktualizacji sesji:
```json
{
    "sessionId": "guid",
    "refToken": "string"
}
```

#### Zarządzanie tokenami
```http
# Odświeżenie tokena
POST /account/usersession/auth/refresh-token
```
Request body:
```json
{
    "sessionId": "guid",
    "userId": "guid"
}
```

#### Informacje o użytkowniku
```http
# Pobranie informacji o użytkowniku z sesji
GET /account/usersession/profile/userInfo/{sessionId}
```

## 📤 Struktura odpowiedzi API
Każdy endpoint zwraca ujednoliconą strukturę odpowiedzi w formacie:

```csharp
public class HttpResponseModel
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public object? Message { get; set; }
}
```

Przykładowa odpowiedź sukcesu:
```json
{
    "success": true,
    "error": null,
    "message": {
        "userId": "123e4567-e89b-12d3-a456-426614174000"
    }
}
```

Przykładowa odpowiedź błędu:
```json
{
    "success": false,
    "error": "User with that username/email already exists.",
    "message": null
}
```

## 🔄 Integracja z pozostałymi serwisami
- Wykorzystanie Gateway Service jako punkt wejścia do systemu

## 📈 Możliwości rozwoju
- Implementacja dwuetapowej weryfikacji (2FA)
- Dodanie integracji z aktywacją konta za pomocą linku wysyłanego na adres e-mail

## 👨‍💻 Autor
Igor Tomaszewski
