# Cleaning Git History

To remove sensitive data from Git history, follow these steps:

## Prerequisites

1. Install git-filter-repo:

```
pip install git-filter-repo
```

## Steps to Clean Git History

1. Make sure you have a backup of your repository before proceeding.

2. Remove the sensitive files from Git history:

```bash
# Clone a fresh copy of your repository (to be safe)
git clone https://github.com/As-if-Newaz/POSBOX.git posbox-clean
cd posbox-clean

# Use git filter-repo to remove sensitive files from history
git filter-repo --path PosBox.MVC/bin/Debug/net8.0/appsettings.json --invert-paths

# Force push the changes
git push origin --force
```

## Alternative: Using BFG Repo Cleaner

If you prefer using BFG:

1. Download BFG from https://rtyley.github.io/bfg-repo-cleaner/

2. Clone a fresh mirror of your repository:
```bash
git clone --mirror https://github.com/As-if-Newaz/POSBOX.git posbox-mirror.git
```

3. Run BFG to replace sensitive data:
```bash
java -jar bfg.jar --replace-text sensitive-patterns.txt posbox-mirror.git
```

Where sensitive-patterns.txt contains patterns of sensitive data to replace.

4. Clean and push:
```bash
cd posbox-mirror.git
git reflog expire --expire=now --all
git gc --prune=now --aggressive
git push --force
```

## Important Notes

- After cleaning history, all collaborators will need to re-clone the repository
- This cannot remove data from forks of your repository
- Consider adding patterns to your .gitignore file to prevent accidentally committing sensitive files in the future