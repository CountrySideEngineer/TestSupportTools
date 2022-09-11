#include <stdio.h>
#include <windows.h>
#include "min_unit.h"
#include "UserHeader.h"
#include "sample_function_001_stub.h"
long outsideVariable;
extern short insideVariable;


//Test target function declare.
int sample_function_001(int input1, int* input2);

//Initialize test stub buffers.
void sample_function_001_utest_SetUp()
{
	subFuncA_init();
}


static char* sample_function_001_utest_2()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, &input2);

	mu_assert(2 == _ret_val);
}

char* sample_function_001_utest_run_all()
{
	mu_run_test("sample_function_001_utest_2", sample_function_001_utest_2);

	return 0;
}

