# Etapa de build
FROM python:3.12.6-slim

# Declarar variable de entorno
ARG PROJECT_NAME=FacturaDto
ENV PROJECT=$PROJECT_NAME

# Crear y usar el directorio de trabajo
WORKDIR /app

# Copiar todo al contenedor
COPY . .

# Instalar dependencias
RUN pip install --no-cache-dir -r requirements.txt

# Exponer el puerto de la API
EXPOSE 8050

# Comando para ejecutar la API
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8050"]
