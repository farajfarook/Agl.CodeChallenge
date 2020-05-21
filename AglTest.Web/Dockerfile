FROM node:12-alpine AS builder
WORKDIR /build
COPY . .
RUN npm i
RUN npm run build

FROM nginx:alpine
COPY --from=builder /build/dist/AglTest-Web /usr/share/nginx/html