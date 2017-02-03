// mathdll.cpp: определяет экспортированные функции для приложения DLL.
//

#include "stdafx.h"
#include "mathdll.h"

namespace MathFuncs
{

	void SuperSort(int* vector)
	{
		vector[0] = vector[0] ^ vector[1];
		vector[1] = vector[1] ^ vector[0];
		vector[0] = vector[0] ^ vector[1];
	}
}

