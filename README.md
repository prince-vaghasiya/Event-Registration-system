# Event Registration System

## Project Description
The Event Registration System is a web application built with .NET Core that allows users to create events, specify registration forms, deadlines, maximum participants, and descriptions. Other users can browse and register for these events. Event creators can manage their events, including editing and deleting them.

## Prerequisites
Before you begin, ensure you have met the following requirements:
- .NET Core SDK (version 8.0 or higher)
- Visual Studio Code or Visual Studio (recommended)

## Installation
1. Clone the repository:
2. Open Visual Studio code and open `EventRegistrationSystem.sln` file of this project
3. Run the project by clicking the run button in Visual Studio code.
4. Access the application in your web browser.

## Usage
1. **Authentication** : Login and signup functionality
2. **Create an Event**: navigate to the "Create Event" page. Fill in the event details, including the title, deadline, maximum number of participants, and a description. it will redirect you to the event page where you can add form fields as you need.

3. **Register for an Event**: Browse the list of available events and click "Details" on the event you want to attend. it will redirect you to the event page where you will see one form fill it and submit it.

4. **Manage Events**: As an event creator, you can edit or delete your events from your event page. also, you can see all responses from other users.

## Features
- Dynamic event creation
- User registration for events
- Event management for creators

## Project Structure
- `Controllers`: Contains controllers responsible for handling HTTP requests.
- `Views`: Contains the views for the application.
- `Models`: Defines the data models used in the application.

