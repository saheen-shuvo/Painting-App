# 🎨 Painting App (Spill Bucket Tool)

This is a simple **Painting Application** developed using **C# Windows Forms**.  
The application includes basic drawing tools and a **Spill Bucket (Flood Fill)** feature similar to image processing software.

The main objective of this project is to demonstrate the **Spill Bucket functionality using two different algorithms**.

---

## ✨ Features

- ✏️ Pencil tool  
- 🧽 Eraser tool  
- ⭕ Draw Ellipse  
- ▭ Draw Rectangle  
- 📏 Draw Line  
- 🎨 Color Picker  
- 🪣 Spill Bucket (Flood Fill)
- 💾 Save drawing as image

---

## 🪣 Spill Bucket Implementation

The spill bucket tool fills a selected pixel and all its neighboring pixels that have the same color.

Two different algorithms are implemented:

### 1️⃣ Stack-based Flood Fill (DFS)
- Uses **Stack**
- Depth First Search approach
- Simple and easy to understand

### 2️⃣ Queue-based Flood Fill (BFS)
- Uses **Queue**
- Breadth First Search approach
- More efficient for large images

Users can select the algorithm using **radio buttons** in the GUI.

---

## 🖥️ Technologies Used

- Language: **C#**
- Framework: **.NET (Windows Forms)**
- IDE: **Visual Studio**

---

## 📷 How to Use

1. Run the application
2. Draw shapes or freehand using the drawing tools
3. Select a color using the color picker
4. Choose a spill bucket algorithm (DFS or BFS)
5. Click on an area to fill it
6. Save the drawing if needed

---

## 📁 Project Structure

