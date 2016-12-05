[1mdiff --git a/RightOrWrong/Assets/BackGround.unity b/RightOrWrong/Assets/BackGround.unity[m
[1mindex f35bad1..c9d9bb8 100644[m
[1m--- a/RightOrWrong/Assets/BackGround.unity[m
[1m+++ b/RightOrWrong/Assets/BackGround.unity[m
[36m@@ -2726,6 +2726,7 @@[m [mGameObject:[m
   - 50: {fileID: 1646033065}[m
   - 60: {fileID: 1646033066}[m
   - 114: {fileID: 1646033067}[m
[32m+[m[32m  - 114: {fileID: 1646033068}[m
   - 114: {fileID: 1646033069}[m
   m_Layer: 9[m
   m_Name: BadGuy0[m
[36m@@ -2956,6 +2957,25 @@[m [mMonoBehaviour:[m
   yMin: -4[m
   MaxScale: 0.07[m
   MinScale: 0.04[m
[32m+[m[32m--- !u!114 &1646033068[m
[32m+[m[32mMonoBehaviour:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_PrefabParentObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInternal: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1646033062}[m
[32m+[m[32m  m_Enabled: 0[m
[32m+[m[32m  m_EditorHideFlags: 0[m
[32m+[m[32m  m_Script: {fileID: 11500000, guid: 60d118fdc8d399f40b56d1ecca442973, type: 3}[m
[32m+[m[32m  m_Name:[m[41m [m
[32m+[m[32m  m_EditorClassIdentifier:[m[41m [m
[32m+[m[32m  moveUp: 273[m
[32m+[m[32m  moveDown: 274[m
[32m+[m[32m  moveLeft: 276[m
[32m+[m[32m  moveRight: 275[m
[32m+[m[32m  dash: 0[m
[32m+[m[32m  mySpeed: 4[m
[32m+[m[32m  dashSpeed: 0[m
[32m+[m[32m  myRigidBody2D: {fileID: 0}[m
 --- !u!114 &1646033069[m
 MonoBehaviour:[m
   m_ObjectHideFlags: 0[m
[36m@@ -3264,7 +3284,6 @@[m [mGameObject:[m
   serializedVersion: 4[m
   m_Component:[m
   - 4: {fileID: 1817600517}[m
[31m-  - 114: {fileID: 1817600516}[m
   m_Layer: 0[m
   m_Name: _GM[m
   m_TagString: Untagged[m
[36m@@ -3272,24 +3291,6 @@[m [mGameObject:[m
   m_NavMeshLayer: 0[m
   m_StaticEditorFlags: 0[m
   m_IsActive: 1[m
[31m---- !u!114 &1817600516[m
[31m-MonoBehaviour:[m
[31m-  m_ObjectHideFlags: 0[m
[31m-  m_PrefabParentObject: {fileID: 0}[m
[31m-  m_PrefabInternal: {fileID: 0}[m
[31m-  m_GameObject: {fileID: 1817600515}[m
[31m-  m_Enabled: 1[m
[31m-  m_EditorHideFlags: 0[m
[31m-  m_Script: {fileID: 11500000, guid: 9bf5c5ec45b1d7a4fb9011e7f10bbe0a, type: 3}[m
[31m-  m_Name: [m
[31m-  m_EditorClassIdentifier: [m
[31m-  player: {fileID: 1866897113}[m
[31m-  badGuys:[m
[31m-  - {fileID: 1646033062}[m
[31m-  - {fileID: 959955207}[m
[31m-  - {fileID: 1599876934}[m
[31m-  - {fileID: 1599876934}[m
[31m-  bullied: {fileID: 1543785916}[m
 --- !u!4 &1817600517[m
 Transform:[m
   m_ObjectHideFlags: 0[m
[1mdiff --git a/RightOrWrong/Assets/Script/Pause.cs b/RightOrWrong/Assets/Script/Pause.cs[m
[1mdeleted file mode 100644[m
[1mindex b9ba74b..0000000[m
[1m--- a/RightOrWrong/Assets/Script/Pause.cs[m
[1m+++ /dev/null[m
[36m@@ -1,54 +0,0 @@[m
[31m-﻿using UnityEngine;[m
[31m-using System.Collections;[m
[31m-[m
[31m-public class Pause : MonoBehaviour[m
[31m-{[m
[31m-[m
[31m-    #region Variabili[m
[31m-    [SerializeField] private GameObject player;[m
[31m-    [SerializeField] private GameObject[] badGuys;[m
[31m-    [SerializeField] private GameObject bullied;[m
[31m-[m
[31m-[m
[31m-    bool showPauseMenu = false;[m
[31m-    bool paused = false;[m
[31m-    #endregion[m
[31m-[m
[31m-    #region Fuznioni per Unity[m
[31m-[m
[31m-    void Awake()[m
[31m-    {[m
[31m-[m
[31m-    }[m
[31m-[m
[31m-    void Update()[m
[31m-    {[m
[31m-        if (Input.GetKeyUp(KeyCode.Escape))[m
[31m-        {[m
[31m-            paused = PauseControl();[m
[31m-        }[m
[31m-    }[m
[31m-[m
[31m-    #endregion[m
[31m-[m
[31m-    #region Funzioni interne[m
[31m-[m
[31m-    bool PauseControl () //se il tempo scorre lo blocca e mette pausa a vero e viceversa[m
[31m-    {[m
[31m-        if (Time.timeScale == 0f)[m
[31m-        {[m
[31m-            Time.timeScale = 1f;[m
[31m-            return false;[m
[31m-        }[m
[31m-        else[m
[31m-        {[m
[31m-            Time.timeScale = 0f;[m
[31m-            return true;[m
[31m-        }[m
[31m-    }[m
[31m-[m
[31m-    #endregion[m
[31m-[m
[31m-[m
[31m-[m
[31m-}[m
[1mdiff --git a/RightOrWrong/Assets/Script/Pause.cs.meta b/RightOrWrong/Assets/Script/Pause.cs.meta[m
[1mdeleted file mode 100644[m
[1mindex b52632e..0000000[m
[1m--- a/RightOrWrong/Assets/Script/Pause.cs.meta[m
[1m+++ /dev/null[m
[36m@@ -1,12 +0,0 @@[m
[31m-fileFormatVersion: 2[m
[31m-guid: 9bf5c5ec45b1d7a4fb9011e7f10bbe0a[m
[31m-timeCreated: 1480948939[m
[31m-licenseType: Free[m
[31m-MonoImporter:[m
[31m-  serializedVersion: 2[m
[31m-  defaultReferences: [][m
[31m-  executionOrder: 0[m
[31m-  icon: {instanceID: 0}[m
[31m-  userData: [m
[31m-  assetBundleName: [m
[31m-  assetBundleVariant: [m
[1mdiff --git a/RightOrWrong/Library/CurrentLayout.dwlt b/RightOrWrong/Library/CurrentLayout.dwlt[m
[1mindex 6fa198c..1cd3a14 100644[m
Binary files a/RightOrWrong/Library/CurrentLayout.dwlt and b/RightOrWrong/Library/CurrentLayout.dwlt differ
[1mdiff --git a/RightOrWrong/Library/InspectorExpandedItems.asset b/RightOrWrong/Library/InspectorExpandedItems.asset[m
[1mindex 3ef10bf..0636ac4 100644[m
Binary files a/RightOrWrong/Library/InspectorExpandedItems.asset and b/RightOrWrong/Library/InspectorExpandedItems.asset differ
[1mdiff --git a/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll b/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll[m
[1mindex e722d5e..fd652b5 100644[m
Binary files a/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll and b/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll differ
[1mdiff --git a/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll.mdb b/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll.mdb[m
[1mindex 5b995c0..c6fb693 100644[m
Binary files a/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll.mdb and b/RightOrWrong/Library/ScriptAssemblies/Assembly-CSharp.dll.mdb differ
[1mdiff --git a/RightOrWrong/Library/assetDatabase3 b/RightOrWrong/Library/assetDatabase3[m
[1mindex 39d3fbd..ef4ae8d 100644[m
Binary files a/RightOrWrong/Library/assetDatabase3 and b/RightOrWrong/Library/assetDatabase3 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000002000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000002000000000000000[m
[1mindex 70855f4..da770b7 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000002000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000002000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000003000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000003000000000000000[m
[1mindex 810db49..a0b76d9 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000003000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000003000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000004000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000004000000000000000[m
[1mindex a2a3a50..84e1f9c 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000004000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000004000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000004100000000000000 b/RightOrWrong/Library/metadata/00/00000000000000004100000000000000[m
[1mindex c252c87..7f8edf5 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000004100000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000004100000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000005100000000000000 b/RightOrWrong/Library/metadata/00/00000000000000005100000000000000[m
[1mindex dd25885..ee0b321 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000005100000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000005100000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000006000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000006000000000000000[m
[1mindex e37d38a..672aab4 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000006000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000006000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000006100000000000000 b/RightOrWrong/Library/metadata/00/00000000000000006100000000000000[m
[1mindex 83a64c4..e2220cb 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000006100000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000006100000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000007000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000007000000000000000[m
[1mindex f1886c7..7036852 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000007000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000007000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000007100000000000000 b/RightOrWrong/Library/metadata/00/00000000000000007100000000000000[m
[1mindex 90d4b34..75bb7aa 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000007100000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000007100000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000008000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000008000000000000000[m
[1mindex c0b433f..aac9eea 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000008000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000008000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000008100000000000000 b/RightOrWrong/Library/metadata/00/00000000000000008100000000000000[m
[1mindex 71f5b0c..de1765e 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000008100000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000008100000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/00000000000000009000000000000000 b/RightOrWrong/Library/metadata/00/00000000000000009000000000000000[m
[1mindex 2f608be..fe6ebe4 100644[m
Binary files a/RightOrWrong/Library/metadata/00/00000000000000009000000000000000 and b/RightOrWrong/Library/metadata/00/00000000000000009000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/0000000000000000a000000000000000 b/RightOrWrong/Library/metadata/00/0000000000000000a000000000000000[m
[1mindex c68a3ef..02df1e8 100644[m
Binary files a/RightOrWrong/Library/metadata/00/0000000000000000a000000000000000 and b/RightOrWrong/Library/metadata/00/0000000000000000a000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/0000000000000000a100000000000000 b/RightOrWrong/Library/metadata/00/0000000000000000a100000000000000[m
[1mindex 8991176..64f71dd 100644[m
Binary files a/RightOrWrong/Library/metadata/00/0000000000000000a100000000000000 and b/RightOrWrong/Library/metadata/00/0000000000000000a100000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/0000000000000000b000000000000000 b/RightOrWrong/Library/metadata/00/0000000000000000b000000000000000[m
[1mindex dfcdb0f..daa63ed 100644[m
Binary files a/RightOrWrong/Library/metadata/00/0000000000000000b000000000000000 and b/RightOrWrong/Library/metadata/00/0000000000000000b000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/00/0000000000000000c000000000000000 b/RightOrWrong/Library/metadata/00/0000000000000000c000000000000000[m
[1mindex 6a6bc43..f19564b 100644[m
Binary files a/RightOrWrong/Library/metadata/00/0000000000000000c000000000000000 and b/RightOrWrong/Library/metadata/00/0000000000000000c000000000000000 differ
[1mdiff --git a/RightOrWrong/Library/metadata/9b/9bf5c5ec45b1d7a4fb9011e7f10bbe0a b/RightOrWrong/Library/metadata/9b/9bf5c5ec45b1d7a4fb9011e7f10bbe0a[m
[1mdeleted file mode 100644[m
[1mindex 0c74985..0000000[m
Binary files a/RightOrWrong/Library/metadata/9b/9bf5c5ec45b1d7a4fb9011e7f10bbe0a and /dev/null differ
[1mdiff --git a/RightOrWrong/Library/metadata/9b/9bf5c5ec45b1d7a4fb9011e7f10bbe0a.info b/RightOrWrong/Library/metadata/9b/9bf5c5ec45b1d7a4fb9011e7f10bbe0a.info[m
[1mdeleted file mode 100644[m
[1mindex 40e265f..0000000[m
Binary files a/RightOrWrong/Library/metadata/9b/9bf5c5ec45b1d7a4fb9011e7f10bbe0a.info and /dev/null differ
