# Event Management — Examen Angular & .NET

Application de gestion d'événements composée d'une API ASP.NET Core (Clean Architecture + Dapper) et d'un frontend Angular.

## Prérequis

| Outil | Version utilisée |
|---|---|
| .NET SDK | 8.0 |
| Node.js | 20.x ou supérieur (testé avec npm 11.9.0) |
| Angular CLI | 21.2.7 |
| SGBD | MySQL 8.x |

## Installation

### 1. Cloner le dépôt

```bash
git clone <url-du-depot>
cd examen-web-2026
```

### 2. Créer la base de données

1. Démarrer un serveur MySQL local (host `localhost`, utilisateur `root`, mot de passe `root` par défaut — voir `examen-web-back/Api/appsettings.json`).
2. Exécuter le script de création de la base et des tables (`sql/CREATE_DB.sql` contient déjà `CREATE DATABASE event_management;`) :

   ```bash
   mysql -u root -p < sql/CREATE_DB.sql
   ```

   > **Linux** : sur Ubuntu/Debian, l'utilisateur `root` MySQL utilise souvent le plugin `auth_socket` (pas de mot de passe classique). Si la commande ci-dessus échoue avec un accès refusé, utiliser :
   > ```bash
   > sudo mysql < sql/CREATE_DB.sql
   > ```
   > Ou créer un utilisateur dédié avec mot de passe et adapter la chaîne de connexion (voir étape 3) :
   > ```bash
   > sudo mysql -e "CREATE USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'root'; GRANT ALL PRIVILEGES ON event_management.* TO 'root'@'localhost'; FLUSH PRIVILEGES;"
   > ```

3. (Optionnel mais recommandé) Charger les données de démonstration (utilisateurs, rôles, événements) :

   ```bash
   mysql -u root -p event_management < sql/SQL_DATA.sql
   ```

   > **Linux** (si `auth_socket` toujours actif) : `sudo mysql event_management < sql/SQL_DATA.sql`

> **Alternative avec DBeaver (GUI)** : si MySQL est installé mais que le client `mysql` en ligne de commande n'est pas pratique (ex: PopOS, ou préférence pour une interface graphique), les scripts peuvent être exécutés directement depuis DBeaver :
> 1. Créer une nouvelle connexion MySQL dans DBeaver (host `localhost`, port `3306`, utilisateur/mot de passe selon votre installation).
> 2. Ouvrir `sql/CREATE_DB.sql` dans DBeaver (`Fichier > Ouvrir un fichier` ou glisser-déposer dans l'éditeur SQL), puis exécuter le script complet avec **Alt+X** (ou clic droit > *Execute SQL Script*). Cela crée la base `event_management` et toutes les tables.
> 3. Se connecter ensuite spécifiquement à la base `event_management` (rafraîchir la connexion ou naviguer dans l'arborescence à gauche), ouvrir `sql/SQL_DATA.sql` de la même façon et l'exécuter avec **Alt+X** pour charger les données de démonstration.
>
> ⚠️ Ne pas utiliser le bouton *Execute SQL Statement* (Ctrl+Enter) qui n'exécute qu'une seule requête à la fois — utiliser *Execute SQL Script* (Alt+X) pour exécuter tout le fichier d'un coup.

### 4. Configurer la chaîne de connexion (si besoin)

La chaîne de connexion se trouve dans `examen-web-back/Api/appsettings.json` :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=event_management;Uid=root;Pwd=root;"
}
```

Adapter `Uid`/`Pwd`/`Server` selon votre configuration locale.

## Lancement du Backend

```bash
cd examen-web-back/Api
dotnet restore
dotnet run
```

L'API démarre par défaut sur `http://localhost:5145`.

## Lancement du Frontend

```bash
cd examen-web-front
npm install
npm start
```

L'application Angular démarre sur `http://localhost:4200`.

> Le frontend pointe vers l'API via `examen-web-front/src/app/environments/environment.ts` (`apiUrl: 'http://localhost:5145'`).

## Comptes de test

Si le script `sql/SQL_DATA.sql` a été exécuté (étape 3 de l'installation), les comptes suivants sont disponibles. Tous partagent le même mot de passe : **`hash1`**.

| Email | Rôle(s) |
|---|---|
| gandalf@example.com | SuperAdmin, Member |
| merlin@example.com | Admin, Member |
| arthur@example.com | Admin, Member |
| tony.stark@example.com | CampManager, Member |
| bruce.wayne@example.com | Cook, Member |
| lara.croft@example.com | Cook, Member |
| geralt@example.com | Member |
| jon.snow@example.com | Member |
| leia.organa@example.com | Member |
| indiana.jones@example.com | CampManager, Member |

Se connecter via la page **Connexion** (`/auth/login`) avec l'un de ces emails et le mot de passe `hash1`.

Il est également possible de créer un nouveau compte via la page **Inscription** (`/auth/register`).

## Fonctionnalités principales

- Inscription / connexion avec authentification JWT
- Liste des événements (publics pour les visiteurs, publics + privés pour les utilisateurs authentifiés)

## Architecture

```
examen-web-back/
├── Api/             # Points d'entrée HTTP (minimal API), routage, JWT
├── Core/             # Modèles métier, UseCases, interfaces de gateways
└── Infrastructure/   # Repositories Dapper, gateways, accès MySQL

examen-web-front/
└── src/app/
    ├── core/components/  # Composants Angular (login, register, event, nav-bar, home)
    ├── core/services/    # Services Angular (AuthService, EventService) — gestion de l'état
    └── core/models/      # Modèles TypeScript
```
