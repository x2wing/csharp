#pragma once
#include <stdexcept>
using namespace std;

namespace MathFuncs
{
	extern "C" { __declspec(dllexport) void SuperSort(int* vector); }
}