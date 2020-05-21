FROM node:12-alpine AS builder
WORKDIR /app
COPY . . 
RUN npm i
CMD [ "npm", "run", "test:prod" ]