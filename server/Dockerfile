FROM python:3.8-alpine

RUN mkdir /app
WORKDIR /app

# installing python packages
ADD requirements.txt /app/
RUN apk add build-base &&\
    apk add --update unixodbc-dev &&\
    pip install -r requirements.txt

# installing ODBC driver
ADD https://download.microsoft.com/download/b/9/f/b9f3cce4-3925-46d4-9f46-da08869c6486/msodbcsql18_18.0.1.1-1_amd64.apk .
RUN apk add --allow-untrusted msodbcsql18_18.0.1.1-1_amd64.apk

ADD . /app/

EXPOSE 8000

ENTRYPOINT ["python", "server.py"]