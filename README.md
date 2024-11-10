# Payment Integration Project

This is a project for payment integration using ASP.NET Core and Entity Framework to manage payment transactions.

## Instructions

Please follow these steps to run the project in your local environment.

### 1. Clone the Repository

You can clone the repository using the following command:

```bash
git clone https://github.com/NicolasMonta1807/dotnet-payments.git
```

Alternatively, you can download the source code by using the "Download ZIP" button on GitHub.

### 2. Build the Project

To build the project, open a terminal in the project's root directory and execute the following commands:

```bash
dotnet clean
dotnet build
```

### 3. Set Up the Database Connection

The project requires a database to store the transactions. To configure the database connection, follow these steps:

1. Navigate to the `PaymentsIntegration` folder:

    ```bash
    cd PaymentsIntegration
    ```

2. Initialize user secrets (this securely stores the connection string):

    ```bash
    dotnet user-secrets init
    ```

3. Set the connection string for the database. Replace the `server`, `user`, `password`, and `database` values with the appropriate details for your database configuration:

    ```bash
    dotnet user-secrets set "ConnectionStrings:PaymentsConnection" "server=localhost;port=3306;user=user;password=password;database=database"
    ```

### 4. Run the Project

Once everything is set up, you can run the project with the following command:

```bash
dotnet run
```

This will start the server on your local environment, allowing you to test the payment service through the configured API routes.

## License

This project is licensed under the GPL 3.0 License - see the [LICENSE](LICENSE) file for more details.
