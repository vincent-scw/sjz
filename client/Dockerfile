FROM node:alpine AS builder

WORKDIR /app
COPY . .

RUN apk --no-cache add g++ gcc libgcc libstdc++ linux-headers make python
RUN npm install --quiet node-gyp -g

RUN npm install && npm run build

FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

COPY --from=builder /app/dist/ /usr/share/nginx/html/

CMD ["nginx", "-g", "daemon off;"]
