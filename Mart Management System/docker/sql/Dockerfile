FROM mcr.microsoft.com/mssql/server:2019-latest


# Install required tools (sqlcmd)
USER root
RUN apt-get update && apt-get install -y curl gnupg apt-transport-https && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Create init directory
RUN mkdir -p /usr/src/app/init
WORKDIR /usr/src/app

# Copy init scripts into container
COPY init/ /usr/src/app/init/

# Make scripts executable
RUN chmod -R 755 /usr/src/app/init

# Run SQL Server and apply scripts
CMD ["/bin/bash", "-c", "/opt/mssql/bin/sqlservr & sleep 20 && for f in /usr/src/app/init/*.sql; do /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -i $f; done && wait" ]
