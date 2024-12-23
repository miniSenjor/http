# Укажите базовый образ Node.js
FROM node:16-alpine

# Установите рабочую директорию
WORKDIR /app

# Скопируйте файлы package.json и package-lock.json
COPY package*.json ./

# Установите зависимости
RUN npm install

# Скопируйте остальные файлы приложения
COPY . .

# Откройте порт для приложения
EXPOSE 3000

# Команда для запуска приложения
CMD ["npm", "start"]