@echo off
git init
git add README.md
git add .
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/DewzaCSharp/Big-File-Creator.git
git push -u origin main
pause