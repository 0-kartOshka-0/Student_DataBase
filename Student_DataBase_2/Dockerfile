# Используем официальный образ .NET 6 SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Устанавливаем рабочую директорию в контейнере
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем остальные файлы и билдим проект
COPY . ./
RUN dotnet publish -c Release -o out

# Используем официальный образ .NET 6 Runtime для выполнения приложения
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем собранные файлы из предыдущего этапа
COPY --from=build /app/out ./

# Указываем команду запуска приложения
ENTRYPOINT ["dotnet", "Student_DataBase_2.dll"]