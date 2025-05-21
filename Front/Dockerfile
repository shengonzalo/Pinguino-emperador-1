FROM node:16.16.0 as build

WORKDIR /app

COPY package.json /app/package.json
RUN npm install
RUN npm install -g @angular/cli@7.3.10

FROM nginx:1.17.1-alpine
COPY --from=node app/dist/surexport-frontend /usr/share/nginx/html
