def get_visible(grid: list[list[int]], direction: str) -> set[tuple[int, int]]:
    height = len(grid)
    width = len(grid[0])

    axis, start, stop, step, coord_getter = {
        "lr": ("row", 0, width, 1, lambda line, i: (i, line)),
        "rl": ("row", width - 1, -1, -1, lambda line, i: (i, line)),
        "td": ("column", 0, height, 1, lambda line, i: (line, i)),
        "bu": ("column", height - 1, -1, -1, lambda line, i: (line, i)),
    }[direction]

    if axis == "row":
        lines = grid
    else:
        lines = [[row[i] for row in grid] for i in range(width)]

    visible = set()
    for line_index, line in enumerate(lines):
        max = line[start]
        for i in range(start, stop, step):
            if i in [start, stop] or line_index in [0, len(line) - 1]:
                visible.add(coord_getter(line_index, i))
            val = line[i]
            if val > max:
                max = val
                visible.add(coord_getter(line_index, i))

    return visible


def get_scenic_distance(
    grid: list[list[int]], coordinate: tuple[int, int], direction: str
) -> int:
    heights = []
    if direction == "up":
        for row_index, row in enumerate(grid):
            if row_index >= coordinate[1]:
                continue

            heights.append(row[coordinate[0]])
        heights.reverse()
    elif direction == "down":
        for row_index, row in enumerate(grid):
            if row_index <= coordinate[1]:
                continue

            heights.append(row[coordinate[0]])
    elif direction == "right":
        row = grid[coordinate[1]]
        for col_index, col in enumerate(row):
            if col_index <= coordinate[0]:
                continue

            heights.append(col)
    elif direction == "left":
        row = grid[coordinate[1]]
        for col_index, col in enumerate(row):
            if col_index >= coordinate[0]:
                continue

            heights.append(col)

        heights.reverse()

    val = grid[coordinate[1]][coordinate[0]]

    if len(heights) == 0:
        return 0

    for distance, height in enumerate(heights):
        if height >= val:
            return distance + 1

    return len(heights)


with open("input.txt") as f:
    grid = [[int(char) for char in line.strip()] for line in f]

visible_sets = [get_visible(grid, direction) for direction in ["lr", "rl", "td", "bu"]]
all_visible = set.union(*visible_sets)
print(len(all_visible))

scenic_distances = []
for y in range(len(grid)):
    for x in range(len(grid[0])):
        scenic_distances.append(
            get_scenic_distance(grid, (x, y), "up")
            * get_scenic_distance(grid, (x, y), "down")
            * get_scenic_distance(grid, (x, y), "left")
            * get_scenic_distance(grid, (x, y), "right")
        )

print(sorted(scenic_distances, reverse=True)[0])
