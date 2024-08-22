<p align="center">
    <h1 align="center">TOURNEYPRO</h1>
</p>
<p align="center">
    <em>Master Your Tournaments, Enhance Every Match!</em>
</p>
<p align="center">
	<img src="https://img.shields.io/github/license/N0nettt/TourneyPro?style=flat-square&logo=opensourceinitiative&logoColor=white&color=blueviolet" alt="license">
	<img src="https://img.shields.io/github/last-commit/N0nettt/TourneyPro?style=flat-square&logo=git&logoColor=white&color=blueviolet" alt="last-commit">
	<img src="https://img.shields.io/github/languages/top/N0nettt/TourneyPro?style=flat-square&color=blueviolet" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/N0nettt/TourneyPro?style=flat-square&color=blueviolet" alt="repo-language-count">
<p>
<p align="center">
		<em>Developed with the software and tools below.</em>
</p>
<p align="center">
	<img src="https://img.shields.io/badge/XAML-0C54C2.svg?style=flat-square&logo=XAML&logoColor=white" alt="XAML">
</p>

<br><!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary><br>

- [📍 Overview](#-overview)
- [🧩 Features](#-features)
- [🗂️ Repository Structure](#️-repository-structure)
- [📦 Modules](#-modules)
- [🚀 Getting Started](#-getting-started)
  - [⚙️ Installation](#️-installation)
  - [🤖 Usage](#-usage)
  - [🧪 Tests](#-tests)
- [🛠 Project Roadmap](#-project-roadmap)
- [🤝 Contributing](#-contributing)
- [🎗 License](#-license)
- [🔗 Acknowledgments](#-acknowledgments)
</details>
<hr>

## 📍 Overview

TourneyPro is a comprehensive tournament management application designed to streamline the organization and execution of competitive events. Central to its functionality is the ability to manage participant details, create and adjust tournament brackets, and distribute prizes efficiently. The software supports various user roles, including administrators, organizers, and participants, each with tailored access to relevant features such as user authentication, tournament selection, and role-specific interfaces. With its robust database integration, TourneyPro ensures real-time updates and maintains data integrity across all operations, making it an essential tool for efficient and effective tournament management.

---

## 🧩 Features

|    | Feature            | Description |
|----|--------------------|---------------------------------------------------------------|
| ⚙️  | **Architecture**   | Built using WPF for UI and C# for backend logic. Utilizes a layered structure with separate classes for UI, data access, and business logic. |
| 🔩 | **Code Quality**   | Code is modular with clear separation of concerns. Utilizes classes and methods effectively, though specific style and quality metrics aren't provided. |
| 📄 | **Documentation**  | Each file and major component is well-documented with comments explaining the purpose and functionality, aiding in maintainability. |
| 🔌 | **Integrations**   | Integrates with SQL databases for data management. Uses Dapper for ORM to simplify database interactions. |
| 🧩 | **Modularity**     | High modularity with functionality split across multiple XAML and C# files. Classes for participants, tournaments, and database management enhance reusability. |
| 🧪 | **Testing**        | No explicit mention of testing frameworks or tools. Testing strategy is not documented in the provided details. |
| ⚡️ | **Performance**    | Performance specifics aren't detailed, but the use of Dapper suggests efficient data querying. Application likely performs well under normal operational loads. |
| 🛡️ | **Security**       | Basic user session management is implemented. No detailed security measures like encryption or secure data handling mentioned. |
| 📦 | **Dependencies**   | Depends on .NET frameworks, Dapper for ORM, and SQL Server for database management. |
| 🚀 | **Scalability**    | Scalability potential is hinted at with database-driven design and session management, but no specific scalability strategies are discussed. |
```

---

## 🗂️ Repository Structure

```sh
└── TourneyPro/
    ├── DiplomskiRad
    │   ├── AddParticipant.xaml
    │   ├── AddParticipant.xaml.cs
    │   ├── App.config
    │   ├── App.xaml
    │   ├── App.xaml.cs
    │   ├── AssemblyInfo.cs
    │   ├── Classes
    │   ├── DataAccess
    │   ├── DiplomskiRad.csproj
    │   ├── EditParticipant.xaml
    │   ├── EditParticipant.xaml.cs
    │   ├── EditUser.xaml
    │   ├── EditUser.xaml.cs
    │   ├── img
    │   ├── LoginWindow.xaml
    │   ├── LoginWindow.xaml.cs
    │   ├── MainWindow.xaml
    │   ├── MainWindow.xaml.cs
    │   ├── ManagePrizes.xaml
    │   ├── ManagePrizes.xaml.cs
    │   ├── RegisterWindow.xaml
    │   ├── RegisterWindow.xaml.cs
    │   ├── UserSelectTournamentWindow.xaml
    │   ├── UserSelectTournamentWindow.xaml.cs
    │   ├── Window1.xaml
    │   └── Window1.xaml.cs
    ├── DiplomskiRad.sln
    ├── ProjectDocs
    │   ├── DBTables_SS
    │   ├── Diagrams
    │   ├── Diplomski_Luka_Urosevic_56118.docx
    │   ├── Diplomski_Luka_Urosevic_56118.pdf
    │   └── UI_SS
    └── README.md
```

---

## 📦 Modules

<details closed><summary>.</summary>

| File                                                                                   | Summary                                                                                                                                                                                                                                                     |
| ---                                                                                    | ---                                                                                                                                                                                                                                                         |
| [DiplomskiRad.sln](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad.sln) | The DiplomskiRad.sln serves as the central project configuration for the TourneyPro application, orchestrating build settings and project dependencies across different development environments, ensuring consistent compilation and deployment processes. |

</details>

<details closed><summary>DiplomskiRad</summary>

| File                                                                                                                                    | Summary                                                                                                                                                                                                                                                                                                                                                            |
| ---                                                                                                                                     | ---                                                                                                                                                                                                                                                                                                                                                                |
| [AddParticipant.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\AddParticipant.xaml)                               | AddParticipant.xaml serves as the user interface for adding new participants to the system within the TourneyPro application. It features form inputs for participant details and styled interactive elements, enhancing user experience and integration with the applications participant management functionalities.                                             |
| [AddParticipant.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\AddParticipant.xaml.cs)                         | Manages the addition of new participants to a tournament in the TourneyPro system, ensuring email validation and preventing duplicate entries based on email addresses. It interfaces with a database to retrieve and sort existing participants, offering a user-friendly selection process.                                                                      |
| [App.config](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\App.config)                                                 | App.config establishes the database connection for the TourneyPro application, specifying the server, database name, and authentication method. It ensures seamless data interactions essential for managing tournament operations, user registrations, and prize distributions within the applications architecture.                                              |
| [App.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\App.xaml)                                                     | App.xaml serves as the entry point for the TourneyPro application, initializing the user interface with a login window. It configures the application-level resources necessary for maintaining a consistent look and feel across various components within the software.                                                                                          |
| [App.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\App.xaml.cs)                                               | App.xaml.cs initializes the main application settings for the TourneyPro project, managing the startup and overall configuration of the application within the DiplomskiRad module. It serves as the entry point, orchestrating the application lifecycle and environment setup.                                                                                   |
| [AssemblyInfo.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\AssemblyInfo.cs)                                       | Defines the locations for theme-specific and generic resource dictionaries within the TourneyPro application, ensuring that visual resources are correctly managed and accessed, enhancing the applications UI consistency and responsiveness across different windows and user interactions.                                                                      |
| [DiplomskiRad.csproj](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\DiplomskiRad.csproj)                               | Defines the configuration for the TourneyPro application, specifying it as a Windows executable using WPF for the UI. It manages dependencies like Dapper for data access and controls the inclusion and update of various image resources critical for the applications visual elements.                                                                          |
| [EditParticipant.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\EditParticipant.xaml)                             | EditParticipant.xaml provides a user interface for modifying tournament participant details within the TourneyPro application. It features form fields for participant name and email, and a styled button to submit changes, ensuring a user-friendly experience with visual feedback for interaction states.                                                     |
| [EditParticipant.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\EditParticipant.xaml.cs)                       | EditParticipant.xaml.cs facilitates the modification of participant details within the TourneyPro application. It includes functionality to validate email formats and update participant information in the database, ensuring data integrity and user input validation for the management of tournament participants.                                            |
| [EditUser.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\EditUser.xaml)                                           | EditUser.xaml serves as the user interface for modifying user details within the TourneyPro application. It facilitates the editing of usernames, emails, and roles, enhancing user management capabilities by providing a visually guided experience through a form-based layout with interactive elements like buttons and dropdowns.                            |
| [EditUser.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\EditUser.xaml.cs)                                     | EditUser.xaml.cs enables user profile modifications within the TourneyPro application, providing functionalities to update user details such as email, username, and role through a user interface, ensuring input validation and database interaction for persisting changes.                                                                                     |
| [LoginWindow.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\LoginWindow.xaml)                                     | Provides a user interface for logging into the TourneyPro system, featuring styled buttons and text fields for username and password input. It includes links for new user registration and visual feedback for user interactions such as mouse hover and button presses.                                                                                          |
| [LoginWindow.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\LoginWindow.xaml.cs)                               | LoginWindow.xaml.cs manages user authentication for the TourneyPro application, directing users to appropriate interfaces based on their roles—admin, organizer, or participant—after successful login. It also provides navigation to the registration interface for new users.                                                                                   |
| [MainWindow.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\MainWindow.xaml)                                       | Serves as the primary interface for managing tournament operations within the TourneyPro system, facilitating participant addition, editing, and removal, bracket creation, and prize management, all within a user-friendly graphical layout enhanced by custom visual styles and data templates for display consistency.                                         |
| [MainWindow.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\MainWindow.xaml.cs)                                 | Manages the main user interface for tournament operations in TourneyPro, handling participant management, bracket creation, and prize distribution based on user roles and tournament status. Integrates with session and database management to ensure real-time updates and user-specific interactions.                                                          |
| [ManagePrizes.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\ManagePrizes.xaml)                                   | ManagePrizes.xaml serves as the user interface for configuring prize distribution in tournaments, allowing users to define prize structures either by percentage or fixed amounts, and manage prize details such as amount, percentage, and placement. It supports prize creation, modification, and deletion within the application.                              |
| [ManagePrizes.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\ManagePrizes.xaml.cs)                             | Manages tournament prize distribution within the TourneyPro application, enabling dynamic prize calculation based on participant numbers and entry fees. Features include custom prize allocation, automatic prize distribution based on predefined rules, and real-time adjustments to prize structures. Integrates with the database for prize data persistence. |
| [RegisterWindow.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\RegisterWindow.xaml)                               | RegisterWindow.xaml serves as the user interface for new user registration within the TourneyPro application, featuring fields for username, password, email, and password confirmation, along with a styled registration button to submit user details, enhancing the applications accessibility and user management capabilities.                                |
| [RegisterWindow.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\RegisterWindow.xaml.cs)                         | RegisterWindow.xaml.cs facilitates user registration for the TourneyPro application, handling input validation, user creation, and database interaction. It ensures data integrity and user feedback through comprehensive error handling and success messages, seamlessly transitioning new users to the login window upon successful registration.               |
| [UserSelectTournamentWindow.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\UserSelectTournamentWindow.xaml)       | UserSelectTournamentWindow.xaml serves as the user interface for selecting a tournament within the TourneyPro application. It features a dropdown menu populated with tournament options and a button to proceed, styled with custom visual elements for enhanced user interaction.                                                                                |
| [UserSelectTournamentWindow.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\UserSelectTournamentWindow.xaml.cs) | UserSelectTournamentWindow facilitates user interaction for selecting tournaments. It retrieves and displays tournaments associated with the logged-in user, allowing selection and opening of a tournament in the main application window, enhancing user experience and tournament management efficiency within the TourneyPro system.                           |
| [Window1.xaml](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Window1.xaml)                                             | Window1.xaml facilitates the creation and management of tournaments within the TourneyPro application, offering features to specify tournament details, manage participants, and handle tournament operations such as editing and deleting tournaments, directly impacting user interaction and administrative functionalities.                                    |
| [Window1.xaml.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Window1.xaml.cs)                                       | Window1.xaml.cs facilitates tournament and user management within the TourneyPro application, enabling administrators to create, edit, and delete tournaments and users. It integrates user role verification to display administrative controls and manages tournament entry fees and participant details dynamically.                                            |

</details>

<details closed><summary>DiplomskiRad.Classes</summary>

| File                                                                                                                                  | Summary                                                                                                                                                                                                                                                                                                                                   |
| ---                                                                                                                                   | ---                                                                                                                                                                                                                                                                                                                                       |
| [Bracket.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Bracket.cs)                                       | Bracket.cs defines the Bracket class, managing tournament structures by creating rounds, generating matches, and pairing opponents. It integrates with a database for persistent storage and retrieval, supporting dynamic bracket adjustments and reset functionalities essential for the tournament flow in the TourneyPro application. |
| [Match.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Match.cs)                                           | Manages tournament matches within the TourneyPro application, handling participant assignments, winner determination, and progression to subsequent rounds. It integrates with the database to update match outcomes and participant details, ensuring the tournaments flow and integrity are maintained.                                 |
| [Participant.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Participant.cs)                               | Defines the Participant class within the TourneyPro system, encapsulating properties and methods for managing participant details such as ID, name, and email, crucial for user management and tournament operations across various interfaces and data access layers in the application.                                                 |
| [ParticipantWinnerConverter.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\ParticipantWinnerConverter.cs) | ParticipantWinnerConverter serves as a utility within the TourneyPro application, determining if a participant is the winner of a match by comparing match results, enhancing the applications ability to dynamically update and display tournament standings based on real-time data.                                                    |
| [Prize.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Prize.cs)                                           | Prize.cs defines the Prize class, encapsulating tournament prize details such as placement, amount, and percentage. It links each prize to a specific tournament, facilitating the management of tournament rewards within the TourneyPro applications broader architecture.                                                              |
| [Role.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Role.cs)                                             | Defines the `Role` class within the TourneyPro application, encapsulating role attributes and behaviors essential for user management and access control. It includes constructors and a method for clear textual representation of role data, supporting both direct instantiation and ORM-based data handling.                          |
| [Round.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Round.cs)                                           | The `Round.cs` class manages tournament rounds, facilitating the addition and retrieval of matches, and determining if a round is the final one within its bracket context in the TourneyPro application.                                                                                                                                 |
| [SessionManager.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\SessionManager.cs)                         | SessionManager.cs serves as the central mechanism for managing user sessions within the TourneyPro application, handling tasks such as setting, clearing, and checking the status of user sessions to ensure secure and efficient user interactions across the system.                                                                    |
| [Tournament.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\Tournament.cs)                                 | Defines the `Tournament` class, managing tournament details such as participants, brackets, and prizes. It supports operations like adding participants, announcing winners, and managing payouts, crucial for the applications functionality in organizing and running tournaments efficiently within the TourneyPro system.             |
| [User.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\Classes\User.cs)                                             | Defines the `User` class within the TourneyPro system, encapsulating properties like username, password, email, and role, along with methods to validate these properties and aggregate validation errors, ensuring user data integrity throughout the application.                                                                       |

</details>

<details closed><summary>DiplomskiRad.DataAccess</summary>

| File                                                                                                               | Summary                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| ---                                                                                                                | ---                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| [GlobalConfig.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\DataAccess\GlobalConfig.cs)       | GlobalConfig.cs establishes the database connectivity for the TourneyPro application, managing the retrieval of connection strings and initializing SQL connections, ensuring seamless data access and manipulation across the applications various functionalities.                                                                                                                                                                                                                                          |
| [IDataConnection.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\DataAccess\IDataConnection.cs) | Defines the interface for database operations within the TourneyPro application, encompassing methods for creating, updating, and deleting tournament-related entities such as participants, tournaments, and users, as well as handling authentication and tournament logistics.                                                                                                                                                                                                                             |
| [SqlConnector.cs](https://github.com/N0nettt/TourneyPro/blob/master/DiplomskiRad\DataAccess\SqlConnector.cs)       | Establishing connections to the SQL database using `SqlConnection`.-Performing CRUD (Create, Read, Update, Delete) operations on database entities such as participants and prizes.-Supporting complex data transactions and queries to facilitate features like tournament management and user registration.This file is integral to the repository as it underpins the backend logic for data management, directly impacting the applications performance and reliability in handling data-intensive tasks. |

</details>

---

## 🚀 Getting Started

**System Requirements:**

* **C#**: 10.0

### ⚙️ Installation

<h4>From <code>source</code></h4>

> 1. Clone the TourneyPro repository:
>
> ```console
> $ git clone https://github.com/N0nettt/TourneyPro
> ```
>
> 2. Change to the project directory:
> ```console
> $ cd TourneyPro
> ```
>
> 3. Install the dependencies:
> ```console
> $ dotnet build
> ```

### 🤖 Usage

<h4>From <code>source</code></h4>

> Run TourneyPro using the command below:
> ```console
> $ dotnet run
> ```

### 🧪 Tests

> Run the test suite using the command below:
> ```console
> $ dotnet test
> ```

---

## 🤝 Contributing

Contributions are welcome! Here are several ways you can contribute:

- **[Report Issues](https://github.com/N0nettt/TourneyPro/issues)**: Submit bugs found or log feature requests for the `TourneyPro` project.
- **[Submit Pull Requests](https://github.com/N0nettt/TourneyPro/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.
- **[Join the Discussions](https://github.com/N0nettt/TourneyPro/discussions)**: Share your insights, provide feedback, or ask questions.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your github account.
2. **Clone Locally**: Clone the forked repository to your local machine using a git client.
   ```sh
   git clone https://github.com/N0nettt/TourneyPro
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to github**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
8. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="center">
   <a href="https://github.com{/N0nettt/TourneyPro/}graphs/contributors">
      <img src="https://contrib.rocks/image?repo=N0nettt/TourneyPro">
   </a>
</p>
</details>

---

## 🔗 Acknowledgments

- List any resources, contributors, inspiration, etc. here.

[**Return**](#-overview)

---
