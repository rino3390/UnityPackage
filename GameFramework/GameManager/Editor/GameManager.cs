﻿using GameFramework.GameManager.Editor.Setting;
using GameFramework.GameManager.Editor.Utility;
using GameFramework.RinoUtility.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework.GameManager.Editor
{
	public class GameManager
	{
		object content; //用於讀取其他MenuTree的內容

		private bool NeedRebuildTree; //重新繪製MenuTree

		[Button(ButtonSizes.Large, Icon = SdfIconType.CardList, Name = "卡片管理")]
		[ButtonGroup("Menu")]
		public void CardWindow() { }

		public List<string> ButtonNames = new();

		[MenuItem("Tools/GameManager")]
		public static void OpenWindow()
		{
			var tabSetting = RinoEditorUtility.FindAsset<GameManagerTabSetting>();
			// var window = GetWindow<GameManager>();
			// window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 700);
		}

		private GameEditorMenu menu;

		//初始化
		// protected override void Initialize()
		// {
		// menu = CreateInstance<GameEditorMenu>();
		// }

		//繪製整個Window，所以可以在這裡進行布局，姑且這樣認知
		// protected override void OnGUI()
		// {
		// 	
		// 	foreach(var t in ButtonNames)
		// 	{
		// 		if (SirenixEditorGUI.SDFIconButton(t,10f,SdfIconType.CardList))
		// 		{
		// 			Debug.Log("Button " + t + " clicked");
		// 		}
		// 	}
		// 	
		// 	if(( NeedRebuildTree || menu.NeedRebuildTree ) && Event.current.type == EventType.Layout)
		// 	{
		// 		ForceMenuTreeRebuild();
		// 		NeedRebuildTree = false;
		// 		menu.NeedRebuildTree = false;
		// 	}
		//
		// 	DrawEditor(2);
		//
		// 	base.OnGUI();
		// }
		//
		// //繪製右邊編輯視窗
		// protected override void DrawEditors()
		// {
		// 	if(( NeedRebuildTree || menu.NeedRebuildTree ) && Event.current.type == EventType.Layout || Event.current.type == EventType.Repaint)
		// 	{
		// 		content = this.MenuTree.Selection.SelectedValue;
		// 		NeedRebuildTree = false;
		// 		menu.NeedRebuildTree = false;
		// 	}
		//
		// 	DrawEditor(1);
		// }
		//
		// //獲取要繪製的目標 (顯示在右邊編輯視窗)
		// protected override IEnumerable<object> GetTargets()
		// {
		// 	List<object> targets = new List<object>();
		// 	targets.Add(null);
		// 	targets.Add(content);
		// 	targets.Add(base.GetTarget());
		// 	return targets;
		// }

		// //新增條目到菜單
		// protected override OdinMenuTree BuildMenuTree()
		// {
		// 	// OdinMenuTree tree = menu.menuTree;
		//
		// 	// return tree;
		// }
		//
		// protected override void OnBeginDrawEditors()
		// {
		// 	// menu.BeginDraw(MenuTree);
		// }

		// private void SwitchMenu<T>() where T: GameEditorMenu
		// {
		// 	// menu = CreateInstance<T>();
		// 	NeedRebuildTree = true;
		// 	MenuTree.Selection.Clear();
		// }
	}
}