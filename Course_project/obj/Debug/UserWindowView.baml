   M S B A M L   `   `   `  ����I  ECourse_project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Course_projectCourse_project  5   	   #    Course_project.UserWindowView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Course_project     5   	     � Title$)  #Прохождение тестов��5   	   $	��700��6$   $	��900��61    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�5
       � 	Resources 5      ��%    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.4* �6   �� 5      $
��0.6* �6    � ColumnDefinitions 5   
   �� 5      $
��0.7* �6   �� 5      �� 5      $��0.45* �6   .+�5   
   �� $��0��6   $��0��6#   $7��1Ваши последние результаты:��60   .��5      �� .��5      �� $
��auto��6    �� CanVerticallyScroll . .+�5      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     HorizontalGridLinesBrush
 �����	   RowBackground
	 �����
   AlternatingRowBackground

 �����   CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .  ��6   �� *EndedResults+ �� SelectedItem 6E   �� *SelectedResult+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ Тест : ��5$   3   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   ��  �� Path$ 	Name_Test� �� Mode$ TwoWay=�  5%       . $ Результат: ��5&   2    6G   �� $ Score_Result�$ TwoWay=�  5'       . $ Результат: ��5(   1    6F   �� $ Description_Result�$ TwoWay=�5*      5,      5-      5/      �� 52   
   $��1��6   $��0��6#   $*��$Ваши текущие тесты: ��60   .��53      ��  .  J� RenderTransformOrigin$ 0.502,0.676&�64    54       �� 
TargetType �� Property �� Value��%�   '��       �� 55      " ��6   .�56      ��  � ��$ 25��63   �� 57       � ��$ 25��62   �� 58       � ��
 �����67   5:      5;      .+�5<      �  � Orientation$ 
Horizontal=�6   $
��Left=�65   .+�5=      �� $	��170��6    $��19��6,   Начать тест:5?      �� 5@       � ToolTip$ Начать тест��6   ��64   �� *StartTestCommand+.��5A      �� $��Resource/start.png��6    5B      5C      �� 5E       . .+�5F      �� $��0��5G   8   $��Visible=�6H   $	��100��5H   (   ��5F      �� *CurrentTests+ 5G      �� *SelectedTest+��5I      �� 5J      .R�5K   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5L   &   �� .+�5M   *   �� ��64   �� $ 	Name_Test�5O   '   5P   #   5Q      5S      5T      5W      5X      �� 5[   
   $��1��6   $��3��6!   ��5\      �� *SelectedTest.Name_Test+.��5`      �� .��5a      ��  . ��. $��Visible=�6^   .+�5b      �� ��6   �� *TestQuestions+��5c      �� 5d      .R�5e   "   �� .+�5f   &   �� $ 
Horizontal=�61   .+�5g   *   �� $��Right=�6X   ��64   �� $ Text_Question�6X   �� 5h   *   ��5i   0   �� $ Picture�5j   '   �� 5k   &   ��6/   �� *	Answers+��5l   *   �� 5m   .   .R�5n   2   �� .+�5o   6   �� ��6@   �� $ Text_Answer��� 5p   6    d� 	IsChecked 6?   �� $ Correct�$ TwoWay=�6r   5q   3   5r   /   5t   '   5u   #   5v      5x      �� 5y      $��32��6@   ��6   �� *EndTestCommand+.��6L   Закончить тест6\   5z      5{      5      �� 5�   
   $��3��6   $��0��6#   $��Вы вошли как:��60    � DataContext 6G   �� *User+.��5�      �� $ Vertical=�6   .+�5�      � $ 
Horizontal=�6   .+�5�      �� .��6   Логин:6&   �� 5�      ��6   �� *
Login_User+6>   5�      � 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Студент:6&   �� 5�      ��6   �� *FirstName_User+6B   �� 5�      ��6   �� *LastName_User+6@   5�      � 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Группа:6%   �� 5�      ��6   �� *
Group_User+6=   5�      5�      5�      5�      5�      