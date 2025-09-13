# Etapa 1: Compilar la aplicación React
FROM node:22.18.0-alpine AS build
WORKDIR /app

# Copiar archivos de paquete e instalar dependencias
COPY webapp/package.json webapp/package-lock.json ./
RUN npm install

# Copiar el resto del código fuente de la aplicación web
COPY webapp/ ./

# Construir la aplicación para producción
RUN npm run build

# Etapa 2: Servir la aplicación con Nginx
FROM nginx:stable-alpine

# Copiar la compilación de producción desde la etapa de 'build'
COPY --from=build /app/dist /usr/share/nginx/html

# Copiar la configuración personalizada de Nginx
COPY webapp/nginx.conf /etc/nginx/conf.d/default.conf

# Exponer el puerto 80
EXPOSE 80

# Iniciar Nginx
CMD ["nginx", "-g", "daemon off;"]
