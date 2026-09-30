# Bibliotheque.Demo — état final après la Journée 2

Ce projet correspond au code du démonstrateur tel qu'il se trouve à l'issue de
**toutes** les démonstrations live de la Journée 2 du Module 2 (guide formateur
`Journee2_EFCore_Modelisation`) : Séquence 2 (entité unique), Séquence 3
(introduction de la relation Livre/Auteur) et Séquence 4 (aperçu Add/SaveChanges).

## Ce qui n'est PAS dans ce dossier

Les migrations EF Core (`Migrations/*.cs`) ne sont pas fournies en tant que
fichiers : ce sont des artefacts générés par l'outil `dotnet ef`, pas du code
qu'on écrit à la main. Pour reconstituer l'état exact obtenu en salle, exécutez,
dans l'ordre, les commandes utilisées pendant la journée :

```bash
cd Bibliotheque.Demo
dotnet restore

# Séquence 2 — entité unique (migration créée puis, comme en Séquence 3, abandonnée)
dotnet ef migrations add InitLivres

# Séquence 3 — on repart d'une base vide plutôt que de convertir le texte existant
dotnet ef database drop -f
dotnet ef migrations add AjoutEntiteAuteur

dotnet ef database update
dotnet run
```

`dotnet run` applique les migrations restantes (`Database.MigrateAsync()`),
insère le jeu de données de test s'il est absent, puis exécute à la suite
toutes les requêtes LINQ démontrées pendant la journée, et termine par l'ajout
de *Domain-Driven Design* pour Eric Evans (Séquence 4).

## Fichiers fournis

- `Livre.cs`, `Auteur.cs` — entités, état final (relation, pas le champ texte de la Séquence 2)
- `BibliothequeContext.cs` — DbContext avec la configuration Fluent API
- `Program.cs` — orchestration complète, dans l'ordre du déroulé de la journée
- `Bibliotheque.Demo.csproj` — packages NuGet nécessaires

## Non vérifié par exécution

Cet environnement ne dispose pas du SDK .NET (ni d'accès réseau pour restaurer
les packages) : ce code n'a donc pas pu être compilé ni exécuté ici. Il reprend
fidèlement les extraits du guide formateur, assemblés et relus avec attention,
mais une vérification par `dotnet run` reste recommandée avant utilisation en
salle.
