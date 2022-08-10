#include <stdio.h>
#include "min_unit.h"

int test_run = 0;

char* sample_function_003_utest_run_all();

int main()
{
	mu_run_all_test("sample_function_003_utest_run_all", sample_function_003_utest_run_all);

	return 0;
}
