# VinylVerse

[Here will be photo/video]

VinylVerse is a feature-rich, multi-window studio application designed for managing vinyl records, artists, tracks, and sales. Built with a modern, intuitive graphical interface and robust back-end support, VinylVerse empowers users—from casual music enthusiasts to professional administrators—to manage their music collections and related transactions seamlessly.

# Features

### Modern Graphical Interface & Custom Branding

- **Customizable Logo & Studio Name:**  
  Easily change the application logo and edit your studio’s name to suit your brand identity.

- **Multi-Window Interface:**  
  Enjoy an interface with multiple windows that follows industry-standard layouts—including a primary menu and a secondary "quick-access" menu featuring the most important functions.

- **Keyboard Accelerators:**  
  Built-in keyboard shortcuts (accelerators) streamline workflow, making navigation fast and efficient.

- **Intuitive, Responsive Design:**  
  The interface is designed to be visually appealing and intuitive, offering both a light (day) layout and a dark (night) layout—with the possibility for automatic layout selection after login.

[Here will be photo/video]



### User Management & Security

- **Login & Registration:**  
  The application starts with a secure login screen that also offers registration.  
  - The very first registered user becomes the system administrator.

- **Database-Driven Data:**  
  All data is stored in a database. If no database is found at startup, a standard database is automatically created.

- **Administrator Controls:**  
  Administrators can:
  - Manage and reassign user roles (including promoting others to administrator).
  - Remove an administrator (except if they were the first registered) or resign (provided they designate another administrator).

- **User Profiles:**  
  Every user can manage their own profile, and logged-in users have their personal and address data loaded automatically. Non-logged-in users must provide their data on the spot.

- **Secure Password Storage:**  
  Passwords are not stored in plain text. Instead, only a double SHA-1 hash (`SHA1(SHA1(password))`) is saved in the database.

[Here will be photo/video]

### Record, Artist & Track Management

- **Comprehensive Management:**  
  The studio allows you to create, edit, and delete information about:
  - **Records (Vinyls)**
  - **Artists**
  - **Tracks**

- **Automatic Track Assignment:**  
  When assigning tracks to a record, if an artist is specified, all tracks on that record are automatically assigned to that artist.

- **Image Integration:**  
  An optional feature displays a list of images (sourced from a specified folder and its subdirectories) when adding a new track or record, allowing you to select cover art easily.

- **Inventory Control:**  
  Manage multiple records and their associated data from a centralized administration panel.

[Here will be photo/video]

### Sales, Shop & Additional Functionalities

- **Integrated Shop:**  
  A built-in shop enables the sale of records and tracks.  
  - Each user starts with an initial balance.
  - User profiles maintain records of all purchased items.

- **Printing & Backup Options:**  
  Options are available to print receipts or purchase summaries, and to perform backups (for example, generating ZIP archives of user or system files).

- **Multi-Database Support:**  
  VinylVerse is designed to work with various database systems (MySQL, PostgreSQL, SQLite, MongoDB, etc.), making it adaptable to different environments.

- **Inter-Application Communication:**  
  A dedicated window allows two instances of VinylVerse (on different computers) to communicate, enabling virtual transfers (including monetary transactions, sales records, and track images) between users.

- **Demo Mode:**  
  A demo version of the program is available with certain features disabled and an unlock key for full functionality.

[Here will be photo/video]

# Localization & Terms

- **Multilingual Support:**  
  Choose the language for the application from several available options.

- **Terms & Conditions:**  
  A dedicated button provides access to the Terms of Service. The TOS are loaded from a text file, with a default short version restored if the file is missing.

# Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/VinylVerse.git
    ```

2. **Open the solution** in Visual Studio and build the project.

3. **Configure your database** settings as needed (support for MySQL, PostgreSQL, SQLite, MongoDB, etc.).

4. **Run the application** to experience VinylVerse’s full feature set.

[Here will be photo/video]

# Usage

Upon launch, VinylVerse displays a login screen with registration capability. Once logged in:

- **Administrators** can manage records, artists, and tracks, as well as handle user roles and assignments.
- **Regular Users** can browse available records, view detailed information, and make purchases via the integrated shop.

For more detailed instructions, refer to the documentation provided in the [Documentation](docs/README.md) folder.

[Here will be photo/video]

# License

VinylVerse is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
