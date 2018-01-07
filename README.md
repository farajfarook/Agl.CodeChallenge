# AglTest
AGL Pet sorting test app

## Builds

Master 

![master](https://travis-ci.org/farajfarook/AglTest.svg?branch=master)

Develop 

![master](https://travis-ci.org/farajfarook/AglTest.svg?branch=develop)


## Introduction

This dotnet core 2.0 demostrative application uses DDD principles for the back end and the front end is developed using Angular

### Projects
 - AglTest.Domain - Domain business logic
 - AglTest.Infratructure - Infrastructure implementation of the logic
 - AglTest.Web - Web APIs and the base server for the Angular application

### Unit Tests
- AglTest.Domain.Tests - Unit tests written in XUnit and Moq for the domain logic
- AglTest.Infrastructure.Tests - Unit tests for the infrasructure implemtations.

## How to run?

 - `cd AglTest.Web`
 - `npm i`
 - `webpack --config=webpack.config.vendor.js`
 - `webpack`
 - `dotnet run`
