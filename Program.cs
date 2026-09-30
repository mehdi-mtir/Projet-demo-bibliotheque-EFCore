using Microsoft.EntityFrameworkCore;
using Bibliotheque.Demo;


using var context = new BibliothequeContext();

await context.Database.MigrateAsync();


if (!await context.Auteurs.AnyAsync())
{
    var evans = new Auteur { Nom = "Eric Evans" };              // Id = 1 — sans livre pour l'instant
    var martin = new Auteur { Nom = "Robert C. Martin" };
    var fowler = new Auteur { Nom = "Martin Fowler" };
    var bloch = new Auteur { Nom = "Joshua Bloch" };
    var beck = new Auteur { Nom = "Kent Beck" };
    var feathers = new Auteur { Nom = "Michael Feathers" };
    var thomas = new Auteur { Nom = "David Thomas" };
    var orwell = new Auteur { Nom = "George Orwell" };
    var saintExupery = new Auteur { Nom = "Antoine de Saint-Exupéry" };

    context.Auteurs.AddRange(evans, martin, fowler, bloch, beck, feathers, thomas, orwell, saintExupery);

    context.Livres.AddRange(
        new Livre { Titre = "Clean Code", Auteur = martin, DateParution = new DateTime(2008, 8, 1), Genre = "Génie logiciel", Disponible = true },
        new Livre { Titre = "Refactoring", Auteur = fowler, DateParution = new DateTime(1999, 7, 8), Genre = "Génie logiciel", Disponible = true },
        new Livre { Titre = "UML Distilled", Auteur = fowler, DateParution = new DateTime(1997, 9, 1), Genre = "Génie logiciel", Disponible = true },
        new Livre { Titre = "Effective Java", Auteur = bloch, DateParution = new DateTime(2001, 5, 1), Genre = "Java", Disponible = false },
        new Livre { Titre = "Extreme Programming Explained", Auteur = beck, DateParution = new DateTime(1999, 10, 5), Genre = "Méthodologie", Disponible = true },
        new Livre { Titre = "Working Effectively with Legacy Code", Auteur = feathers, DateParution = new DateTime(2004, 9, 22), Genre = "Génie logiciel", Disponible = true },
        new Livre { Titre = "The Pragmatic Programmer", Auteur = thomas, DateParution = new DateTime(1999, 10, 30), Genre = "Génie logiciel", Disponible = true },
        new Livre { Titre = "1984", Auteur = orwell, DateParution = new DateTime(1949, 6, 8), Genre = "Roman", Disponible = true },
        new Livre { Titre = "Le Petit Prince", Auteur = saintExupery, DateParution = new DateTime(1943, 4, 6), Genre = "Roman", Disponible = true }
    );

    await context.SaveChangesAsync();
    Console.WriteLine("Données de test insérées.");
}

// Console.WriteLine("\n--- Tous les livres ---");
// var tousLesLivres = await context.Livres.ToListAsync();
// foreach (var l in tousLesLivres) Console.WriteLine($"  {l.Titre}");

// Console.WriteLine("\n--- Livres disponibles, triés par titre ---");
// var disponibles = await context.Livres
//     .Where(l => l.Disponible)
//     .OrderBy(l => l.Titre)
//     .Select(l => new { l.Titre })
//     .ToListAsync();
// foreach (var l in disponibles) Console.WriteLine($"  {l.Titre}");

// Console.WriteLine("\n--- Recherche « Code » dans le titre ---");
// var resultats = await context.Livres
//     .Where(l => l.Titre.Contains("Code"))
//     .ToListAsync();
// foreach (var l in resultats) Console.WriteLine($"  {l.Titre}");

// Console.WriteLine("\n--- Le livre le plus ancien ---");
// var leplusAncien = await context.Livres
//     .OrderBy(l => l.DateParution)
//     .FirstAsync();
// Console.WriteLine($"  {leplusAncien.Titre} ({leplusAncien.DateParution:yyyy})");

Console.WriteLine("\n--- Nombre de livres par genre ---");
var parGenre = await context.Livres
    .GroupBy(l => l.Genre)
    .Select(g => new { Genre = g.Key, Nombre = g.Count() })
    .ToListAsync();
foreach (var g in parGenre) Console.WriteLine($"  {g.Genre} : {g.Nombre}");

// ---------------------------------------------------------------------
// Séquence 3 — Requêtes LINQ sur la relation Livre / Auteur
// ---------------------------------------------------------------------

// Console.WriteLine("\n--- Auteurs et leurs livres (Include) ---");
// var auteursEtLeursLivres = await context.Auteurs
//     .Include(a => a.Livres)
//     .ToListAsync();
// foreach (var a in auteursEtLeursLivres) Console.WriteLine($"  {a.Nom} : {a.Livres.Count} livre(s)");

// Console.WriteLine("\n--- Livres de Martin Fowler ---");
// var livresDeFowler = await context.Auteurs
//     .Where(a => a.Nom == "Martin Fowler")
//     .SelectMany(a => a.Livres)
//     .ToListAsync();
// foreach (var l in livresDeFowler) Console.WriteLine($"  {l.Titre}");

// Console.WriteLine("\n--- Nombre de livres par auteur, décroissant ---");
// var parAuteur = await context.Auteurs
//     .Select(a => new { a.Nom, Nombre = a.Livres.Count })
//     .OrderByDescending(x => x.Nombre)
//     .ToListAsync();
// foreach (var x in parAuteur) Console.WriteLine($"  {x.Nom} : {x.Nombre}");

// Console.WriteLine("\n--- Livres disponibles, avec le nom de l'auteur ---");
// var disponiblesAvecAuteur = await context.Livres
//     .Include(l => l.Auteur)
//     .Where(l => l.Disponible)
//     .Select(l => new { l.Titre, Auteur = l.Auteur.Nom })
//     .ToListAsync();
// foreach (var x in disponiblesAvecAuteur) Console.WriteLine($"  {x.Titre} — {x.Auteur}");

// Console.WriteLine("\n--- Auteurs sans livre ---");
// var sansLivre = await context.Auteurs
//     .Where(a => !a.Livres.Any())
//     .Select(a => a.Nom)
//     .ToListAsync();
// foreach (var nom in sansLivre) Console.WriteLine($"  {nom}");

// ---------------------------------------------------------------------
// Séquence 4 — Aperçu du Module 5 : Add et SaveChanges
// ---------------------------------------------------------------------

// Console.WriteLine("\n--- Ajout d'un livre pour Eric Evans (AuteurId = 1) ---");
// var livre = new Livre { Titre = "Domain-Driven Design", AuteurId = 1, DateParution = new DateTime(2003, 8, 22), Genre = "Génie logiciel", Disponible = true };
// context.Livres.Add(livre);
// context.SaveChanges();

// var evansFinal = await context.Auteurs
//     .Include(a => a.Livres)
//     .FirstAsync(a => a.Nom == "Eric Evans");
// Console.WriteLine($"  {evansFinal.Nom} : {evansFinal.Livres.Count} livre(s)");
