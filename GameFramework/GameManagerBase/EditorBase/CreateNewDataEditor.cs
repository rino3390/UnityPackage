﻿using GameFramework.GameManagerBase.Extension;
using GameFramework.GameManagerBase.SOBase;
using GameFramework.RinoUtility.Editor;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEngine;
using GUID = GameFramework.RinoUtility.Misc.GUID;

namespace GameFramework.GameManagerBase.EditorBase
{
	public abstract class CreateNewDataEditor<T>: GameEditorMenu where T: SODataBase
	{
		protected abstract string dataRoot { get; }

		protected abstract string dataTypeLabel { get; }

		private string _dataRoot => dataRoot + "/";

		[UsedImplicitly]
		private string _createDataGroupLabel => "新增" + dataTypeLabel;

		[BoxGroup("$_createDataGroupLabel")]
		[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
		public T Data;

		[Required("程式端尚未實作資料整合方法")]
		[ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Hidden), HideLabel]
		[PropertySpace(10)]
		private DataSet<T> _dataSet;

		private readonly bool addAllDataForMenu;
		private readonly bool drawDelete;

		protected CreateNewDataEditor(bool addAllDataForMenu = true, bool drawDelete = true)
		{
			this.drawDelete = drawDelete;
			this.addAllDataForMenu = addAllDataForMenu;
		}

		protected override void Initialize()
		{
			SetNewData();

			_dataSet = RinoEditorUtility.FindAssetWithInheritance<DataSet<T>>();

			if(_dataSet == null)
			{
				CreateDataSet();
			}
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = SetTree().AddSelfMenu(this, dataTypeLabel);

			if(addAllDataForMenu)
			{
				tree.AddAllAssets<T>(dataTypeLabel,_dataRoot, drawDelete);
			}

			return tree;
		}

		[BoxGroup("$_createDataGroupLabel")]
		[OnInspectorGUI, ShowIf("@!Data.IsDataLegal()")]
		private void CreateNewDataInfoBox()
		{
			SirenixEditorGUI.ErrorMessageBox("資料尚未正確設定");
		}

		[BoxGroup("$_createDataGroupLabel")]
		[Button("Create"), DisableIf("@!Data.IsDataLegal()"), GUIColor(0.67f, 1f, 0.65f)]
		private void CreateNewData()
		{
			if(!Data.IsDataLegal()) return;
			RinoEditorUtility.CreateSOData(Data, _dataRoot + Data.AssetName);
			SetNewData();
			ForceMenuTreeRebuild();
		}

		private void SetNewData()
		{
			Data = CreateInstance<T>();
			Data.Id = GUID.NewGuid();
			var root = _dataRoot.Split('/');
			Data.AssetName = root[^2] + " - " + Data.Id;
		}

		private void CreateDataSet()
		{
			var dataSetType = RinoEditorUtility.GetDerivedClasses<DataSet<T>>().First();

			if(dataSetType == null)
			{
				return;
			}
			
			var dataSet = CreateInstance(dataSetType);
			RinoEditorUtility.CreateSOData(dataSet, "Data/Set/" + typeof(T).Name + "DataSet");
			_dataSet = (DataSet<T>)dataSet;
			ForceMenuTreeRebuild();
		}
	}
}