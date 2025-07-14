# MixItUp

MixItUp is a cross-platform .NET MAUI mobile application for Android and iOS. It offers a complete reference library of cocktails and mocktails, including detailed recipes, ingredients, and preparation instructions.

---

## Features

- Comprehensive catalog of classic and modern cocktails
- Complete list of non-alcoholic mocktails
- Ingredient lists with precise measurements
- Step-by-step preparation instructions
- Serving suggestions and presentation tips
- Built with .NET MAUI for seamless Android and iOS support
- Local data storage for offline access

---

## Purpose

MixItUp is designed for:

- Home bartenders who want to explore new recipes
- Professionals needing a handy reference on the go
- Anyone who wants to learn about cocktail and mocktail preparation

---

## Tech Stack

- .NET MAUI (for cross-platform Android/iOS)
- C#
- XAML for UI
- MVVM pattern for maintainability
- Local storage/database support
- Optional integration modules (Google Authentication stubbed for demo)

---

## Project Structure

- Common/: Shared utilities and constants
- Converters/: UI data converters for XAML bindings
- CustomControls/: Reusable UI components
- GoogleAuthentication/: Stubbed module for optional sign-in integration
- Helpers/: Utility classes
- Interface/: Abstractions for services and models
- LocalDatabaseModel/: Models for local data storage
- Model/: Domain models for recipes and ingredients
- Pages/: XAML pages and view logic
- Utility/: Extension methods and supporting utilities

---

## Getting Started

1. Clone the repository:
   git clone https://github.com/YourUsername/MixItUp.git

2. Open in Visual Studio 2022+ with .NET MAUI workload installed.

3. Build and deploy to:
   - Android emulator or device
   - iOS simulator or device (Mac required)

4. Run the app and browse the catalog of cocktails and mocktails.

---

## Notes

- This version is a developer demo and may use placeholder data.
- Real production deployments can connect to cloud backends or sync with online recipe sources.
- The GoogleAuthentication module is included as a scaffold for optional sign-in features.

---

## License

This project is licensed for demonstration and educational use. Commercial use requires permission. See LICENSE for details.
