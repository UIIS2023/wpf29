   M S B A M L   `   `   `  ����A  =pisnzm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:pisnzm.Framepisnzm.Frame  5   	       pisnzm.Frame.frmIgrac   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:pisnzm.Frame     5   	     � Title$  dodaj/izmeni Igrac��5   	   $	��350��6$   $	��300��61   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��53* �6   �� 5      $	��47* �6   .+�5   
   �� $��Ime igraca: ��6   $
��Left=�6'   $��	10,10,0,0q�6B   $	��Top=�6U    J� RenderTransformOrigin$ 	0.5,0.614&�6m   �� 5   
   $��Prezime igraca: ��6   $
��Left=�6+   $��	10,37,0,0q�6F   $	��Top=�6Y   �� 5   
   -   5         ��Name$ txtImeIgraca��5      $
��Left=�6(   $��
103,14,0,0q�6C    �� TextWrapping$
 Wrap=�6W   $�� ��6k   $	��Top=�6t   $	��120��6�   $��2��6�   �� 5   
   -   5         ��Name$ cmbKK��5      $
��Left=�6"   $��103,182,0,0q�6=   $	��Top=�6R   $	��120��6j   $��2��6v    �� SelectedValuePath$
 KKID��6�   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sacuvaj��6%   $
��Left=�67   $��
17,284,0,0q�6R   $	��Top=�6f   6~   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkazi��6$   $
��Left=�65   $��
81,284,0,0q�6P   $	��Top=�6d   $��1��6|   6�   �� 5   
   $��Starost igraca: ��6   $
��Left=�6+   $��	10,63,0,0q�6F   $	��Top=�6Y   �� 5   
   $��Visina igraca: ��6   $
��Left=�6*   $��	10,89,0,0q�6E   $	��Top=�6X   �� 5   
   $��Tezina igraca: ��6   $
��Left=�6*   $��
10,116,0,0q�6E   $	��Top=�6Y   �� 5   
   $��Pozicija igraca: ��6   $
��Left=�6,   $��
10,142,0,0q�6G   $	��Top=�6[   �� 5   
   -   5        $ cmbMenadzer��5      $
��Left=�6(   $��103,222,0,0q�6C   $	��Top=�6X   $	��120��6p   $��2��6|   $ 
MenadzerID��6�   �� 5   
   -   5        $ txtPrezimeIgraca��5      $
��Left=�6,   $��
103,41,0,0q�6G   $
 Wrap=�6[   $�� ��6o   $	��Top=�6x   $	��120��6�   $��2��6�   �� 5   
   -   5        $ txtStarostIgraca��5      $
��Left=�6,   $��
103,67,0,0q�6G   $
 Wrap=�6[   $�� ��6o   $	��Top=�6x   $	��120��6�   $��2��6�   �� 5   
   -   5        $ txtVisinaIgraca��5      $
��Left=�6+   $��
103,93,0,0q�6F   $
 Wrap=�6Z   $�� ��6n   $	��Top=�6w   $	��120��6�   $��2��6�   �� 5   
   -	   5        $ txtTezinaIgraca��5      $
��Left=�6+   $��103,120,0,0q�6F   $
 Wrap=�6[   $	��Top=�6o   $	��120��6�   $��2��6�   �� 5   
   -
   5        $ txtPozicijaIgraca��5      $
��Left=�6-   $��103,146,0,0q�6H   $
 Wrap=�6]   $	��Top=�6q   $	��120��6�   $��2��6�   �� 5   
   $
��KK: ��6   $
��Left=�6   $��
21,180,0,0q�6:   $	��Top=�6N   �� 5   
   $��
Menadzer: ��6   $
��Left=�6%   $��
10,218,0,0q�6@   $	��Top=�6T   5!      5"      