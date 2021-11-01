#!/bin/bash

function run(){
    dotnet tool install --global minicover

    dotnet clean ./src/Okta.Sdk.sln
    dotnet restore ./src/Okta.Sdk.sln
    dotnet build ./src/Okta.Sdk.sln

    minicover instrument

    # add bamtest command

    minicover uninstrument

    minicover htmlreport
}

function clean(){
    if [[ -d ./coverage-hits ]]; then
        printf "deleting ./coverage-hits\r\n"
        rm -fr ./coverage-hits
    fi
    if [[ -d ./coverage-html/ ]]; then
        printf "deleting ./coverage-html\r\n"
        rm -fr ./coverage-html
    fi
    if [[ -f ./coverage.json ]]; then
        printf "deleting coverage.json\r\n"
        rm coverage.json
    fi
}

COMMAND=$1

if [[ -z $1 ]]; then
    COMMAND="run"
fi

$COMMAND
