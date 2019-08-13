FROM mcr.microsoft.com/mssql-tools:latest
# RUN apt-get update \
#     &&  apt-get install --no-install-recommends -y ca-certificates curl apt-transport-https software-properties-common 

# RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
#     && curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | tee /etc/apt/sources.list.d/msprod.list \
#     && apt-get update \
#     && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev
    
ENV DB_DBNAME=
ENV DB_USER=
ENV DB_PASSWORD=
ENV DB_HOST=
ENV DB_PORT=1433
ENTRYPOINT [ "/execute-script.sh" ]
COPY ./execute-script.sh /execute-script.sh
RUN chmod +x /execute-script.sh
ENV FILE=



