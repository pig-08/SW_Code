using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

[UnityEditor.CustomEditor(typeof(PlayerStateSO))]
public class StateSOEditor : UnityEditor.Editor
{
    [SerializeField] private VisualTreeAsset editorUI = default;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        editorUI.CloneTree(root);

        DropdownField dropdown = root.Q<DropdownField>("ClassDropDownField");
        CreateDorpdownList(dropdown);

        return root;

    }

    private void CreateDorpdownList(DropdownField dropdown)
    {
        dropdown.choices.Clear();
        List<string> derivedTypes = GetStateFromAssembly(typeof(PlayerState));
        dropdown.choices.AddRange(derivedTypes);

    }

    private static List<string> GetStateFromAssembly(Type targetType)
    {
        Assembly assembly = Assembly.GetAssembly(targetType);
        List<string> derivedTypes = assembly.GetTypes()
            .Where(type => type.IsClass
            && type.IsAbstract == false
            && type.IsSubclassOf(typeof(PlayerState)))
            .Select(type => type.FullName)
            .ToList();
        return derivedTypes;
    }
}
