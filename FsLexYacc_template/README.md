# FsLexYacc Template

[GitHub link](https://github.com/PatrickMatthiesen/PSD_Assignments/tree/main/FsLexYacc_template)

This is a Template for the assignments in PRDAT. It will allow you to compile the `.fsy` and `.fsl` using `dotnet build` in the console. Most IDE builders might not work as they use specialized build commands.
It also allows you to run the entire thing from an `.fsx` file, so you don't need to mess around in the console.

You can also just use the `Program.fs` file if you don't want to use the `.fsx`.

## How to set it up

1. Change name of the folder to whatever you want (maybe the assignment name).

2. Change name of `.sln` to the same as the folder name.
3. Change name of the project folder (the one next to the `.sln` file).
4. Change name of the `.csproj` file.
5. Replace the `.fsy` and `.fsl` with your own and add the rest of the files you need.
6. Update the file names in the `.csproj`. Make sure to update the module name too.

    ```csproj
    <FsYacc Include="CPar.fsy">
        <OtherFlags>--module CPar</OtherFlags>
    </FsYacc>
    <FsLex Include="CLex.fsl">
        <OtherFlags>--unicode</OtherFlags>
    </FsLex>
    ```

7. Include the files you just added. Make sure to use the order that is used in the commands from the exercise.
   > example where order is like: Absyn.fs CPar.fs CPar.fs...
   >
   > `fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs`
   >
   > Feel free to ignore the `.dll` as a NuGet package takes care of it.

    ```csproj
        <Compile Include="Absyn.fs" />
        <Compile Include="CPar.fs" />
        <Compile Include="CLex.fs" />
        <Compile Include="Parse.fs" />
        <Compile Include="Machine.fs" />
        <Compile Include="Comp.fs" />
        <Compile Include="ParseAndComp.fs" />
    ```

8. Run `dotnet restore`
9. Run `dotnet build` every time you change your `.fsy` and `.fsl` files.
    > I haven't tested it, but you might be able to compile the files using `dotnet run` too.

## Using .fsx

1. Make sure to `#load` all the files you need, in the order you added them in the `.csproj`.
2. Just use the `.fsx` as you are used to
   > If you haven't used it before, then you can run each line for it self by having the cursor on it or run multiple lines by highlighting them, and then press `alt + enter`.
3. You don't need to use `;;` in this file but you do in the terminal.
