# Revit Model Profiler
<p align="center">
<img width="1918" height="1020" alt="Screenshot 2025-10-12 191912" src="https://github.com/user-attachments/assets/725aea48-0db8-4697-8fa7-1c7f6a324a53" />
</p>

**Revit Model Profiler** is a C# add-in for Autodesk Revit designed to help BIM managers, project leads, and Revit users identify families that may be negatively impacting model performance.  
It scans all placed family instances in a project and provides a clear, interactive report on their geometric complexity.

> This tool helps you answer the question:  
> **"Which families are making my model slow?"**

---

## Features

### Comprehensive Analysis
- Scans all `FamilyInstance` elements in the entire project.

### Key Performance Metrics
Reports on:

- **Instance Count** – How many times each family is placed.  
- **Face Count** – Number of faces for families with BRep solid geometry.  
- **Triangle Count** – Number of triangles for families with mesh geometry.  
- **Contextual Data** – Includes the family Category and identifies *In-Place* families.

### Interactive UI
- Click on column headers to **sort** the results.  
- Use the **search bar** to quickly filter by family name or category.  
- Select a family in the grid to enable the **"Select in Model"** button.

### In-Model Selection
Instantly select all instances of a problematic family in your Revit model to see where they are located.

### Data Export
Export the analysis results to a `.csv` file for sharing or further review in Excel.

---

## Installation

You can install the **Revit Model Profiler** in two ways:
- Using the **simple installer** (recommended)
- Or **manual file installation**

### Easy Installation (Recommended)
1. Go to the **Releases** page of this GitHub repository.  
2. Download the latest `RevitModelProfiler_Setup_vX.X.exe` file.  
3. Run the installer and follow the on-screen instructions.  
4. Start **Autodesk Revit 2025** — the **"Model Profiler"** panel will appear in the **"Add-Ins"** tab.

### Manual Installation
1. Go to the **Releases** page and download `RevitModelProfiler.zip`.  
2. Unzip the contents — you will find two files:
   - `RevitModelProfiler.dll`  
   - `RevitModelProfiler.addin`  
3. Copy both files into your Revit Addins folder: %AppData%\Autodesk\Revit\Addins\2025
4. Important:  
Right-click `RevitModelProfiler.dll` → **Properties** → check **"Unblock"** (if visible) → click **OK**.  
This ensures Windows trusts the file.  
5. Start **Autodesk Revit 2025**.

---

## How to Use

1. Open any Revit project.  
2. Go to the **Add-Ins** tab on the ribbon.  
3. Click **"Analyze Families"** in the **Model Profiler** panel.  
4. The **Model Profiler** window will appear and automatically run the analysis.  
5. Interact with the results — sort, search, or select families to investigate them in your model.

---

## For Developers

This project is written in **C#** using **.NET 8.0** for the **Revit 2025 API**.

### Setup
1. Clone this repository to your local machine.  
2. Open `RevitModelProfiler.sln` in **Visual Studio 2022**.  
3. Add Revit API References:
- In *Solution Explorer*, expand the project and right-click on:
  ```
  Dependencies > Assemblies > Add Assembly Reference...
  ```
- Browse to your Revit 2025 installation directory (usually):
  ```
  C:\Program Files\Autodesk\Revit 2025
  ```
- Select `RevitAPI.dll` and `RevitAPIUI.dll` and add them.  
4. Set the build configuration to **Release**.  
5. Build the solution (**Ctrl + Shift + B**).

### Post-Build Event
The project includes a **post-build event** that automatically copies:
- The compiled `.dll`
- The `.addin` manifest file  

...into the correct Revit Addins folder for rapid testing without manual file copying.

---

### Creating the Installer

1. The repository includes an `installer_script.iss` file for building the installer.  
2. Install **[Inno Setup](https://jrsoftware.org/isinfo.php)**.  
3. Update the `Source` paths in the `[Files]` section of the script to match your output directory.  
4. Right-click the `.iss` file → **Compile** to generate the setup `.exe` file.
