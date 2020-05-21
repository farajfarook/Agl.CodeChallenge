# AglTest
AGL Pet Sorting [![Build Status](https://travis-ci.org/farajfarook/AglTest.svg?branch=master)](https://travis-ci.org/farajfarook/AglTest)

## Introduction

This dotnet core demostrative application uses DDD principles for the back end and the front end is developed using Angular

```

+---------------------------------------------------------------+
|                                                               |
|                           traefik                             |
|               (mapped to http://localhost:8080)               |
|                                                               |
+------------+------------------------------------+-------------+
             ^                                    ^
             |                                    |
             |  Route /api/v1/*                   |  Route /
             |                                    |
             |                                    |
     +-------+---------+                 +--------+--------+
     |                 |                 |                 |
     |                 |                 |                 |
     |     Pets API    |                 |   Angular App   |
     |   AGLTest.Api   |                 |   AGLTest.Web   |
     |                 |                 |                 |
     |                 |                 |    NginX Box    |
     |                 |                 |                 |
     +-----------------+                 +-----------------+

```

### Projects
 - AglTest.Api - Api based on Command Handler Pattern using `Enbiso.NLib.Cqrs`
 - AglTest.Domain - Domain objects and logic
 - AglTest.Infratructure - Infrastructure implementation. API calls and Repos
 - AglTest.Web - Pure angular application using serice based store pattern using `BehaviourSubjects`

### Unit Tests
- AglTest.Domain.Tests - Unit tests written in `XUnit` and `NSubstitute` for the domain logic
- AglTest.Infrastructure.Tests - Unit tests for the infrasructure source using `XUnit` and `NSubstitute`.

### Integration Tests
- AglTest.IntegrationTests - BDD based integration tests using `Cucumber` and `Chai`

## How to run?

 - `docker-compose up`
 - Application http://localhost:8088/
 - Swagger http://localhost:8088/api/v1/swagger
