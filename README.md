# Taquin Game - WPF

This game was created as a team project during my second semester in software development. Both versions, one in WPF and the other in the console, are available.

## Part #1: New GIT Branch and Class Library (.NET Core) 

This part involves creating a new GIT branch and a class library to store the `JeuTaquin.cs` file.

### Instructions:
- Label the `master` branch (from TP1) as `Version 1.0`.
- From this label, create a new branch named `Version2`. Push this new branch so your teammate can retrieve it.
- Add a new Class Library (.NET Core) project named `JeuxLib` to the current solution. Move the `JeuTaquin.cs` file to this new library.
- Modify the `JeuTaquinConsole` project to include this new library and ensure the game works properly.

## Part #2: WPF Application Project (.NET Core) - Form-based Sliding Puzzle 

This part involves designing a visual form interface for the sliding puzzle game. The game logic should not be repeated here as it is centralized in the previously created library (Part #1). Here are the interface requirements and instructions:

### When the grid is solved:
#### Instructions:
- Add a new WPF App (.NET Core) project named `JeuTaquinWPF` to the current solution.
- Structure according to MVVM.
- Texts should be `14px` in font size and bold.
- Add a reference to the `JeuxLib` library.
- Declare a `JeuTaquin` object (in the fields) in your form and use it (call its members).
- Arrows should move the numbers to the empty box. Use the provided arrow images (Microsoft - Docs, 2022) with this statement.

    - TextBox  
    - Button  
    - Label for all cases  
    - GroupBox with Grid 5x4  
    - Image  
    - Label to display "Found!!!" during resolution  

- To quit, the player can click on the X.
- At the application startup, the grid should be shuffled. Hint: Load event or in the constructor.
- Ensure that the game functions similarly to the console version.

## Part #3: Multi-form Management - Options 

This part involves adding an additional form to the `JeuTaquinWPF` project to support options. Here is the interface to design and display when the user clicks on the "Settings..." button:

### Instructions:
- Add a form named `frmParamètres` and place the visual components. Texts are `14px` in font size and bold, except for CheckBoxes which are in `12px`.
- In the `JeuxLib` library, with the `JeuTaquin` class, create a new class named `Options` as follows:
  - Add an `Options` property (of type `Options`) to the `JeuTaquin` class.
- The properties of the `Options` class should be associated with the form via `DataContext` and for each visual field via `Binding`.
- Options `Chiffres` and `Images`: Allows viewing either numbers or pieces of images (images are attached to the statement). Example:

    - CheckBox  
    - RadioButton Button  

    - Mode Numbers Mode Images  

- The `Colored Background` option (numbers only): When this option is enabled, the background color turns green when the number is in the correct position in the grid. Example:

- `Sound on Success` option: When this option is enabled, the `success.mp3` sound is played when the grid is solved. The sound must be implemented in the `MainWindow.xaml` form. Use the `MediaPlayer` object with the `Open` and `Play` methods. You must create a `Sounds` folder and place the `.MP3` file in it. Always copy the sound files with the executable for it to work.
