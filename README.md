# Projekt zaliczeniowy z przedmiotu "Programowanie w środowisku ASP.NET"
## Instrukcja instalacji
1. Aby skonfigurować połączenie z bazą danych należy w pliku **appsettings.json** zamienić **localhost\\SQLEXPRESS** na własny łańcuch połączenia z bazą danych wykorzystywany w Microsot SQL Server Management Studio
2. Po otwarciu projektu w terminalu należy wykonać następujące komendy:  
    **dotnet ef database update --context AppDbContext**  
    **dotnet ef database update --context IdentityContext**    
3. W przypadku gdy wystąpił błąd i folder "migrations" jest pusty należy wykonać następujące komendy:  
   **dotnet ef migrations add init --context AppDbContext**    
   **dotnet ef migrations add Identity --context IdentityContext**    
   Następnie ponownie wykonać komendy z punktu 2  
4. Aplikacja jest gotowa do uruchomienia  
## Konta użytkowników    
1. Konto administratora:  
  Login: **admin@gmail.com**  
  Hasło: **Test123!** 
2. Konta użytkowników:  
  Login: **kowalski@gmail.com**  
  Hasło: **Test123!**  
  Login: **nowak@gmail.com**  
  Hasło: **Test123!**  
## Testowanie funkcjonalności REST      
1. Ścieżka obsługująca kontroler REST - **/api/matches** natomiast dla Swaggera - **/swagger**   
2. Format do testowania metody PUT:  
  **id: 1**    
  **{**    
    **"id": 1,**    
    **"hostId": 2,**    
    **"guestId": 4,**    
    **"date": "2023-01-22T13:52:23.100Z",**    
    **"tickets_amount": 8,**    
    **"price": 40**    
  **}**    
3. Format do testowania metody POST:  
  **{**    
    **"hostId": 3,**    
    **"guestId": 1,**    
    **"date": "2023-01-22T13:58:48.100Z",**    
    **"tickets_amount": 18,**    
    **"price": 43**    
  **}**    
4. Dla metod GET(id) oraz DELETE:  
  **id = 2**    
