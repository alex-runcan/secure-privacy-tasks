# Table of Contents

1. [Introduction](#introduction)
2. [Project Setup](#project-setup)
3. [Solution Presentation](#solution-presentation)
4. [Products Explorer](#products-explorer)
5. [Binary String](#binary-string)
6. [GDPR Considerations](#gdpr-considerations)

# Introduction

This project represents a small .NET/C# web application with an Angular front-end, using MongoDB as database, allowing storage and retrieval of products, and checking if a binary string satisfies the conditions presented in [this section](#binary-string).

# Project Setup

The following sections will present the prerequisites to build and run the project.

### Prerequisites

- Docker or MongoDb local server
- .Net8 SDK
- Node ^20.0.0
- Angular CLI ^17.3.0
- Visual Studio / Rider / VS code(with C# Dev Kit)
- Compass for exploring the database

### Actual Steps

1. Run `docker-compose up -d` inside the `SecurePrivacy.Tasks.Api/iac` directory, this will pull the latest image for mongo, will create a container with it configuring the provided credentials for accessing it, after everything is completed, you can use Compass to check if you can access it using the local connection string `mongodb://admin:adminpassword@localhost:27017/`
2. Open the ```SecurePrivacy.Tasks.sln``` solution in any of the IDEs/Code Editors mentioned in the [prerequisites](#prerequisites) section, and run the ```MongoSeedRunner``` console project, this will open a console where you just have to type in the ```up``` command, and it will run the script for creating the indexes and will add some seed data to be able to play with it, if everything runs smoothly you should see the same info logs as in the bellow image:
   
![Console log outpus](https://github.com/user-attachments/assets/e9db73ba-c873-4134-ab93-05b599fd5be2)

3. Now you can run the ```API``` project using the ```Http``` configuration, after the API starts, you can explore it's endpoints using [Swagger](http://localhost:5138/swagger/index.html)
4. The last step would be to run the ```SecurePrivacy.Tasks.FE``` Angular project, first open it in VS Code, open a terminal and run the ```npm ci``` command to install the ```node_modules```, after they were installed, just run the ```npm run start``` command, and you should be able to access the FE at ```http://localhost:4200/products```
5. Enjoy the App 🚀

# Solution Presentation

In the following section, the general and specific considerations that were taken into account when developing the solution will be presented:

## Architecture

The application is built based on the 3TIER architecture (FE, API, DB) using HTTP as communication protocol between the FE and the API, and Mongo Driver for accessing the database Tier. On the API side, the Clean Architecture approach was used, as you can see from the bellow diagram:

![SecurePrivacyArchitecture drawio (2)](https://github.com/user-attachments/assets/87c8a062-dedf-4a74-8c5b-a8b87f5ede82)

Next, the 2 features of the application will be presented and detailed.

# Products Explorer
This feature allows users to browse products, sort by Price, and filter by Rating. To improve performance on these 2 operations, a compound index according to the ESR(Equality, Sort, Range) rule was added to the Rating and Price.

![Products Grid](https://github.com/user-attachments/assets/45717f35-0ae8-4263-b583-f559bb995206)

It also allows users to add new products, the form validates all the fields before allowing to submit the product details.

# Binary String

For the binary string checker, the following constraints are validated:
* Equal number of 0's and 1's.
* For every prefix, the number of 1's is not less than the number of 0's.

The result of the validation is presented below the input after the request is done:

![Validate Binary String](https://github.com/user-attachments/assets/1870adb4-bb52-442c-96a4-d08285f5c1c7)

To ensure that the validation method covers all the scenarios, Unit tests using NUnit3 were added in the ```BinaryStringTesting``` project

![Unit Tests Results](https://github.com/user-attachments/assets/d9daef3e-2061-4bd3-901d-153700073e0f)

# GDPR Considerations
Since the application doesn't store any PII fields that can be subject to the GDPR complience list, the user is only prompted with a modal for GDPR Agreement, and all the data stored will be encrypted with Mongo's default ```AES256-CBC``` encryption algorithm.

![GDPR Consent popup](https://github.com/user-attachments/assets/de03e98b-0d7b-4334-8dd6-eced3383d01d)
