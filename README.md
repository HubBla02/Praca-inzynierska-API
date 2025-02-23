# README - Backend (.NET 8)

## Opis projektu
Aplikacja umożliwia zarządzanie wynajmem pojazdów, w tym rejestrację użytkowników, rezerwację pojazdów, dodawanie opinii oraz dynamiczną kontrolę dostępu w zależności od roli użytkownika. Backend został napisany w **.NET 8** z wykorzystaniem **Entity Framework Core** i **JWT do uwierzytelniania**.

## Kluczowe funkcjonalności
- Rejestracja i logowanie użytkowników (JWT Auth)
- Zarządzanie pojazdami (CRUD + zdjęcia pojazdów)
- Obsługa wypożyczeń (zliczanie dni, sprawdzanie dostępności)
- System opinii (dodawanie ocen i komentarzy, przyznawanie zniżek)
- Dynamiczna zmiana ról użytkownika i ich uprawnień
- Automatyczne zakończanie wypożyczeń po upływie terminu

## Technologie
- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core** (baza danych)
- **JWT Authentication** (uwierzytelnianie)
- **PostgreSQL / SQL Server** (baza danych)
- **Swagger** (dokumentacja API)

## Instrukcja uruchomienia
### 1️⃣ Wymagania
- .NET 8 SDK
- Baza danych PostgreSQL lub SQL Server

### 2️⃣ Klonowanie repozytorium
```sh
git clone https://github.com/twoje-repo/backend.git
cd backend
```

### 3️⃣ Konfiguracja bazy danych
W pliku `appsettings.json` ustaw poprawne dane do bazy danych:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TwojaBaza;User Id=admin;Password=haslo;"
}
```

### 4️⃣ Migracje bazy danych
```sh
dotnet ef database update
```

### 5️⃣ Uruchomienie aplikacji
```sh
dotnet run
```
Backend uruchomi się domyślnie na `http://localhost:5000` (lub `https://localhost:5001`). Możesz sprawdzić API w **Swaggerze**:  
🔗 `http://localhost:5000/swagger`

## Autor
Projekt stworzony przez Huberta Błaszkiewicza w ramach pracy inżynierskiej i dalszego rozwoju zawodowego.

