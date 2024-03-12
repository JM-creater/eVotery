<div align="center">
  <h1>Online Voting System</h1>
</div>

## Description
This project is an Online Voting System developed using ASP.NET Core Web API for the backend and TypeScript for the frontend. The system facilitates secure and efficient voting processes, providing a user-friendly interface for both administrators and voters.

## Features
- **Authentication and Authorization**: Implements secure login mechanisms for administrators and voters to ensure the integrity of the voting process.
- **Voting Management**: Allows administrators to create, manage, and monitor voting events, including setting up candidate lists and defining voting periods.
- **Voter Interface**: Provides a seamless and intuitive interface for voters to cast their votes securely.
- **Result Analysis**: Enables administrators to analyze voting results efficiently, facilitating decision-making processes.

## Technologies Used
### Backend
- **ASP.NET Core Web API**: Provides a robust and scalable backend framework for handling HTTP requests and implementing business logic.

### Frontend
- **TypeScript**: Offers strong typing capabilities and modern features for building interactive and scalable frontend applications.

## Setup Instructions
1. **Backend Setup**:
   - Clone the repository.
   - Navigate to the backend directory.
   - Install dependencies using `dotnet restore`.
   - Configure database settings in `appsettings.json`.
   - Run migrations using `dotnet ef database update` to create the necessary database schema.
   - Start the backend server using `dotnet run`.

2. **Frontend Setup**:
   - Navigate to the frontend directory.
   - Install dependencies using `npm install`.
   - Modify environment variables if necessary.
   - Build the TypeScript code using `npm run build`.
   - Start the frontend server using `npm start`.

## Usage
- **Administrator**:
  - Log in to the admin panel.
  - Create and manage voting events.
  - Monitor voting progress and analyze results.

- **Voter**:
  - Log in to the voting portal.
  - Cast votes securely for candidates.

## Contributions
Contributions to this project are welcome. If you find any issues or have suggestions for improvements, feel free to submit a pull request or open an issue on GitHub.

## License
This project is licensed under the [MIT License](LICENSE). Feel free to use and modify it according to your needs.

## Contact
For any inquiries or support, please contact Joseph Martin Garado at garadojosephmartin98@gmail.com
