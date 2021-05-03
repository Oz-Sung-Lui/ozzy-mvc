FROM ubuntu:20.04

WORKDIR /app

COPY . . 

RUN apt-get update && apt-get install -y && \
    apt-get install -y wget && \
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb &&\
    dpkg -i packages-microsoft-prod.deb &&\
    apt-get update &&\
    apt-get install -y apt-transport-https && \
    apt-get install -y dotnet-sdk-5.0 && \
    apt-get install -y apt-transport-https && \
    apt-get install -y aspnetcore-runtime-5.0 && \
    apt-get install -y dotnet-runtime-5.0 

ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet dev-certs https --trust && \
    dotnet tool install -g dotnet-ef && \
    dotnet-ef migrations add InitialCreate  &&\
    dotnet-ef database update 

EXPOSE 5000

CMD ["dotnet", "watch" ,"run"]