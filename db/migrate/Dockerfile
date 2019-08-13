FROM boxfuse/flyway:5.2.1-alpine

ENV DB_DBNAME=
ENV DB_USER=
ENV DB_PASSWORD=
ENV DB_HOST=
ENV DB_PORT=1433

RUN apk add --upgrade libx11  

ENTRYPOINT [ "/run-migration.sh" ]
COPY ./run-migration.sh /run-migration.sh
RUN chmod +x /run-migration.sh
COPY ./sql /flyway/sql